namespace SmartLabeling.Core.Models
{
    public class ApiSettings 
    {
        public bool IsFakingIoT { get; set; }
     
        public string CameraUrl { get; set; }

        public string SensorsUrl { get; set; }
        
        public string FakeUrl { get; set; }

        public string CameraHub { get; set; }

        public string SensorsHub { get; set; }

        public string FakeCameraHub { get; set; }

        public string FakeSensorsHub { get; set; }



        public int InfraredDistance { get; set; }

        public int ProximityTriggerPin { get; set; }

        public int ProximityEchoPin { get; set; }

        public int ProximityMaxDistance { get; set; }

        public int ProximityDistance { get; set; }

        public int ReadingDelay { get; set; }




        public int ImageWidth { get; set; }

        public int ImageHeight { get; set; }

        public int CaptureDelay { get; set; }

    }
}
