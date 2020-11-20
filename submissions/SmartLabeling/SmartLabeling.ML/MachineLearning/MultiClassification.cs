using Microsoft.ML;
using Microsoft.ML.Data;
using SmartLabeling.ML.Models;
using System.IO;
using System.Linq;

namespace SmartLabeling.ML.MachineLearning
{
    public static class MultiClassification
    {
        public static MulticlassClassificationMetrics Train(string dataPath) 
        {
            MLContext mlContext = new MLContext(seed: 1);

            IDataView data = mlContext.Data.LoadFromTextFile<Reading>(
                path: dataPath, 
                separatorChar: ',', 
                hasHeader: true);

            var shuffledData = mlContext.Data.ShuffleRows(data, seed: 1);
            var split = mlContext.Data.TrainTestSplit(shuffledData, testFraction: 0.3);
            var trainingDataView = split.TrainSet;
            var testingDataView = split.TestSet;

            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", "Label")
                .Append(mlContext.Transforms.Concatenate("Features", new[] { "Temperature", "Luminosity", "Infrared" }));

            var trainer = mlContext.MulticlassClassification.Trainers.LightGbm(labelColumnName: "Label", featureColumnName: "Features")
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            ITransformer mlModel = trainingPipeline.Fit(trainingDataView);

            var predictions = mlModel.Transform(testingDataView);
            var metrics = mlContext.MulticlassClassification.Evaluate(predictions, "Label", "Score", "PredictedLabel");

            return metrics;
        }
    }
}
