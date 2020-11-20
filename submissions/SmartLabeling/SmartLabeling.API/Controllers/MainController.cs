using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using SmartLabeling.API.Helpers;
using SmartLabeling.Core.Models;
using SmartLabeling.ML;
using SmartLabeling.ML.DeepLearning;
using SmartLabeling.ML.MachineLearning;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLabeling.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;
        private readonly ApiSettings _settings;

        //TODO pull assets path to appsettings
        static readonly string assetsRelativePath = @"../../../assets";
        static readonly string assetsPath = PathHelper.GetAbsolutePath(assetsRelativePath);

        static readonly string tagsTsv = Path.Combine(assetsPath, "inputs", "train", "tags.tsv");
        static readonly string inceptionTrainImagesFolder = Path.Combine(assetsPath, "inputs", "train");
        static readonly string inceptionPb = Path.Combine(assetsPath, "inputs", "inception", "tensorflow_inception_graph.pb");
        static readonly string imageClassifierZip = Path.Combine(assetsPath, "outputs", "imageClassifier.zip");
        static readonly string labeled_compare_data = "labeled_sensors_data.csv";

        static readonly string dataRelativePath = @"../../../data";
        static readonly string dataPath = PathHelper.GetAbsolutePath(dataRelativePath);

        public MainController(ILogger<MainController> logger, ApiSettings apiSettings)
        {
            _logger = logger;
            _settings = apiSettings;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation($"GET triggered.");

            return Ok();
        }

        [HttpGet("settings")]
        public IActionResult GetSettings()
        {
            return Ok(_settings);
        }


        public class Some 
        {
            public string FileName { get; set; }
            public int RowsCount { get; set; }
        }


        [HttpGet("datasets")]
        public IActionResult GetDatasets()
        {

            var result = new List<Some>();

            foreach (var file in Directory.GetFiles(Path.Combine(dataPath), "*.csv"))
            {
                result.Add(new Some
                {
                    FileName = Path.GetFileNameWithoutExtension(file),
                    RowsCount = System.IO.File.ReadLines(file).Count()
                }); ;
            }

            return Ok(result);
        }

        [HttpGet("train_ml")]
        public IActionResult TrainMLAsync(int select = 0)
        {
            if (select == 0)
            {
                var metrics = MultiClassification.Train($"{dataPath}/*");

                return Ok(metrics);
            }
            if (select == 1)
            {
                var metrics = MultiClassification.Train( Path.Combine(dataPath, labeled_compare_data));

                return Ok(metrics);
            }

            return BadRequest($"Select parameter {select} is not valid.");
        }

        [HttpPost("save_csv")]
        public IActionResult SaveDatasetAsCsv(List<Reading> readings)
        {
            try
            {
                using var writer = new StreamWriter(Path.Combine(dataPath, $"dataset_{DateTime.UtcNow.Ticks}.csv"));
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(readings);
            }
            catch (Exception)
            {
                return BadRequest("saving failed");
            }

            return Ok("saved successfully");
        }

        [HttpGet("train_inception")]
        public IActionResult ReTrainInception()
        {
            Inception.Model = Inception.LoadAndScoreModel(tagsTsv, inceptionTrainImagesFolder, inceptionPb, imageClassifierZip);
            Console.WriteLine("inception re-trained");

            return Ok("inception re-trained");
        }

        [HttpPost("predict_image")]
        public async Task<IActionResult> ImagePredictAsync()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            string body = await reader.ReadToEndAsync();
            byte[] imageBytes = Convert.FromBase64String(body);
            string result = "";

            try
            {
                var testImage = Path.Combine(inceptionTrainImagesFolder, "capture.jpg");

                Image image = Image.FromStream(new MemoryStream(imageBytes));
                image.Save(testImage, ImageFormat.Jpeg);

                var imageData = new ImageNetData()
                {
                    ImagePath = testImage,
                    Label = Path.GetFileNameWithoutExtension(testImage)
                };

                if (Inception.Model is null)
                {
                    Inception.Model = Inception.LoadModel(tagsTsv, inceptionTrainImagesFolder, inceptionPb, imageClassifierZip);
                }

                var prediction = Inception.Model.Predict(imageData);
                result = prediction.PredictedLabelValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(result);
        }
    }
}
