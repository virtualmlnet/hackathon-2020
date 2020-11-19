using SmartLabeling.API.Helpers;
using SmartLabeling.Core.Interfaces;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using SmartLabeling.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartLabeling.API.Services
{
    public class FakeSensorsService : ISensorsService
    {
        //private readonly Random _random;
        static readonly string assetsRelativePath = @"../../../comparedata";
        static readonly string assetsPath = PathHelper.GetAbsolutePath(assetsRelativePath);
        static readonly string dataset = Path.Combine(assetsPath, "unlabeled_sensors_data.csv");
        public static IList<Reading> Readings { get; set; }

        public static int Index;

        static FakeSensorsService()
        {
            using var reader = new StreamReader(dataset);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            Readings = csv.GetRecords<Reading>().ToList();
        }

        public Task<double> ReadInfrared()
        {
            return Task.Run(() => Readings[Index].Infrared);
        }

        public Task<double> ReadLuminosity()
        {
            Index = (Index + 1) % Readings.Count;
            return Task.Run(() => Readings[Index].Luminosity);
        }

        public Task<double> ReadTemperature()
        {
            return Task.Run(() => Readings[Index].Temperature);
        }
    }
}
