using System.Threading.Tasks;

namespace SmartLabeling.Core.Interfaces
{
    public interface ISensorsService
    {
        Task<double> ReadInfrared();
        Task<double> ReadLuminosity();
        Task<double> ReadTemperature();
    }
}
