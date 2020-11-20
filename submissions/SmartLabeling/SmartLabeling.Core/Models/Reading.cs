namespace SmartLabeling.Core.Models
{
    public class Reading
    {
        public double Temperature { get; set; }
        public double Luminosity { get; set; }
        public double Infrared { get; set; }
        public string CreatedAt { get; set; }
        public string Source { get; set; }
    }
}
