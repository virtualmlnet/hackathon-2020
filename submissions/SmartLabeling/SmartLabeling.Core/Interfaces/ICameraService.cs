using System.Threading.Tasks;

namespace SmartLabeling.Core.Interfaces
{
    public interface ICameraService
    {
        public Task<byte[]> GetImage(int width, int height);
    }
}