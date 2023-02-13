using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using StatsdClient;
using System;

namespace Lead.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dogstatsdConfig = new StatsdConfig
            {
                StatsdServerName = "127.0.0.1",
                StatsdPort = 8125,
            };

            using (var dogStatsdService = new DogStatsdService())
            {
                if (!dogStatsdService.Configure(dogstatsdConfig))
                    throw new InvalidOperationException("Cannot initialize DogstatsD. Set optionalExceptionHandler argument in the `Configure` method for more information.");
            } 

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
