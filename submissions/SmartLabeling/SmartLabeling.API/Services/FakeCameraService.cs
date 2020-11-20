using SmartLabeling.API.Helpers;
using SmartLabeling.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartLabeling.API.Services
{
    public class FakeCameraService : ICameraService
    {
        //TODO pull assets path to appsettings
        static readonly string assetsRelativePath = @"../../../assets";
        static readonly string assetsPath = PathHelper.GetAbsolutePath(assetsRelativePath);
        static readonly string inceptionTrainImagesFolder = Path.Combine(assetsPath, "inputs", "train");

        public Task<byte[]> GetImage(int width, int height)
        {
            var files = Directory.GetFiles(inceptionTrainImagesFolder, "*.jpg"); //TODO accept other image types
            var random = new Random();
            var randomFile = random.Next(files.Length);
            var bytes = File.ReadAllBytesAsync(files[randomFile]);

            return bytes;
        }
    }
}
