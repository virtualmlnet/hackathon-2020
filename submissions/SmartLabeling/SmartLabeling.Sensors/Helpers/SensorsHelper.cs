using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLabeling.Sensors.Helpers
{
    public static class SensorsHelper
    {
        public static double ToLuminosity(byte[] readBuffer)
        {
            var val = ToInt(readBuffer);

            // convert luminosity to percent
            var percentLuminosity = Math.Round((val + 1) / 1024.0 * 100, 2);
            return percentLuminosity;
        }

        public static double ToInfrared(byte[] readBuffer)
        {
            var val = ToInt(readBuffer);

            // convert infrared to percent
            var percentInfrared = Math.Round(100 - (val + 1) / 1024.0 * 100, 2);
            return percentInfrared;
        }

        public static double ToTemperature(byte[] readBuffer)
        {
            var val = ToInt(readBuffer);

            // convert voltage to temperature
            var voltage = val * 3.3;
            voltage /= 1024.0;
            var tempCelsius = Math.Round((voltage - 0.5) * 100, 2);
            return tempCelsius;
        }

        private static int ToInt(byte[] data)
        {
            /* mcp3008 10 bit output */
            int result = data[1] & 0b00000011;
            result <<= 8;
            result += data[2];

            return result;
        }
    }
}
