using Microsoft.ML.Data;

namespace SmartLabeling.ML.Models
{
    class Reading
    {
        [LoadColumn(0)]
        public float Temperature { get; set; }

        [LoadColumn(1)]
        public float Luminosity { get; set; }

        [LoadColumn(2)]
        public float Infrared { get; set; }

        [LoadColumn(3)]
        public string CreatedAt { get; set; }

        [ColumnName("Label"), LoadColumn(4)]
        public string Source { get; set; }
    }
}
