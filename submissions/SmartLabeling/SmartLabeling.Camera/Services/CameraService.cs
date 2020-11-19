using SmartLabeling.Core.Interfaces;
using SmartLabeling.Core.Models;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;

namespace SmartLabeling.Camera.Services
{
    public class CameraService : ICameraService
    {
        readonly ApiSettings _settings;

        public CameraService(ApiSettings settings)
        {
            _settings = settings;
        }

        public Task<byte[]> GetImage(int width, int height)
        {
            return Pi.Camera.CaptureImageJpegAsync(width, height);
        }
    }
}
