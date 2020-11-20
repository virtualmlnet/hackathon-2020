using Microsoft.ML.Data;

namespace SmartLabeling.ML.Models
{
    public class Prediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel { get; set; }

        [ColumnName("Score")]
        public float[] Score { get; set; }
    }
}
