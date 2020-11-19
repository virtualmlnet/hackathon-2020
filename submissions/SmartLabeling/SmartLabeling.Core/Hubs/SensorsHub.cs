using Microsoft.AspNetCore.SignalR;
using SmartLabeling.Core.Interfaces;
using SmartLabeling.Core.Models;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SmartLabeling.Core.Hubs
{
    public class SensorsHub : Hub
    {
        private readonly ISensorsService _sensorsService;
        private static bool _isStreaming;
        private readonly ApiSettings _settings;
        public static string Source { get; set; }

        public SensorsHub(ISensorsService sensorsService, ApiSettings settings)
        {
            _sensorsService = sensorsService;
            _settings = settings;
        }

        public async Task StartSensorsStreaming()
        {
            _isStreaming = true;
            await Clients.All.SendAsync("sensorsStreamingStarted", "started...");
        }

        public async Task StopSensorsStreaming()
        {
            _isStreaming = false;
            await Clients.All.SendAsync("sensorsStreamingStopped");
        }

        public ChannelReader<Reading> SensorsCaptureLoop()
        {
            var channel = Channel.CreateUnbounded<Reading>();
            _ = WriteToChannel(channel.Writer);
            return channel.Reader;

            async Task WriteToChannel(ChannelWriter<Reading> writer)
            {
                while (_isStreaming)
                {
                    try
                    {
                        var luminosity = await _sensorsService.ReadLuminosity();
                        await Task.Delay(_settings.ReadingDelay);

                        var temperature = await _sensorsService.ReadTemperature();
                        await Task.Delay(_settings.ReadingDelay);

                        var infrared = await _sensorsService.ReadInfrared();

                        var createdAt = DateTime.Now.ToString("yyyyMMddhhmmssff");

                        var reading = new Reading
                        {
                            Luminosity = luminosity,
                            Temperature = temperature,
                            Infrared = infrared,
                            CreatedAt = createdAt,
                            Source = Source ?? string.Empty
                        };

                        await writer.WriteAsync(reading);
                        await Clients.All.SendAsync("sensorsDataCaptured", $"{luminosity}, {temperature}, {infrared}, {createdAt}, {Source}");
                    }
                    catch (Exception)
                    {
                        await Clients.All.SendAsync("sensorsDataNotCaptured");
                    }

                    await Task.Delay(_settings.ReadingDelay);
                }
            }
        }

        public void ChangeSource(string source)
        {
            Source = source.Trim();
        }
    }
}
