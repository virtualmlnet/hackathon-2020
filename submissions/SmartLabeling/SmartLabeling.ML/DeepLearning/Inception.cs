using System.Linq;
using Microsoft.ML;

namespace SmartLabeling.ML.DeepLearning
{
    public class Inception
    {
        private const string OUTPUT_LAYER = "softmax2_pre_activation";
        private const string INPUT_LAYER = "input";

        private static readonly string LabelToKey = nameof(LabelToKey);
        private static readonly string ImageReal = nameof(ImageReal);
        private static readonly string PredictedLabelValue = nameof(PredictedLabelValue);
        private static readonly string PredictedLabel = nameof(PredictedLabel);

        private static readonly MLContext mlContext;

        public static PredictionEngine<ImageNetData, ImageNetPrediction> Model { get; set; }

        static Inception()
        {
            mlContext = new MLContext();
        }

        public static PredictionEngine<ImageNetData, ImageNetPrediction> LoadModel(string tsv, string imagesFolder, string inceptionModel, string modelLocation)
        {
            var model = mlContext.Model.Load(modelLocation, out var modelSchema);
            var predictor = mlContext.Model.CreatePredictionEngine<ImageNetData, ImageNetPrediction>(model);

            return predictor;
        }

        public static PredictionEngine<ImageNetData, ImageNetPrediction> LoadAndScoreModel(string tsv, string imagesFolder, string inceptionModel, string modelLocation)
        {
            var data = mlContext.Data.LoadFromTextFile<ImageNetData>(path: tsv, hasHeader: false);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: LabelToKey,
                    inputColumnName: nameof(ImageNetData.Label))
                .Append(mlContext.Transforms.LoadImages(
                    outputColumnName: INPUT_LAYER,
                    imageFolder: imagesFolder,
                    inputColumnName: nameof(ImageNetData.ImagePath)))
                .Append(mlContext.Transforms.ResizeImages(
                    outputColumnName: INPUT_LAYER,
                    imageWidth: ImageNetSettings.imageWidth,
                    imageHeight: ImageNetSettings.imageHeight,
                    inputColumnName: INPUT_LAYER))
                .Append(mlContext.Transforms.ExtractPixels(
                    outputColumnName: INPUT_LAYER,
                    interleavePixelColors: ImageNetSettings.channelsLast,
                    offsetImage: ImageNetSettings.mean))
                .Append(mlContext.Model.LoadTensorFlowModel(inceptionModel).ScoreTensorFlowModel(
                    inputColumnNames: new[] { INPUT_LAYER },
                    outputColumnNames: new[] { OUTPUT_LAYER },
                    addBatchDimensionInput: true))
                .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(
                    labelColumnName: LabelToKey,
                    featureColumnName: OUTPUT_LAYER))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue(PredictedLabelValue, PredictedLabel))
                .AppendCacheCheckpoint(mlContext);

            ITransformer model = pipeline.Fit(data);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<ImageNetData, ImageNetPrediction>(model);
            var trainData = model.Transform(data);
            mlContext.Model.Save(model, trainData.Schema, modelLocation);

            return predictionEngine;
        }
    }
}
