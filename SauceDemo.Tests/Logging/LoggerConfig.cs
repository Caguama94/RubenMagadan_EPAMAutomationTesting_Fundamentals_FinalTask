using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace SauceDemo.Tests.Logging
{

    public static class LoggerConfig
    {
        private static bool _initialized;

        public static void Init()
        {
            if (_initialized) return;

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .WriteTo.File(
                path: "logs/test-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                shared: true,
                outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] (P{ProcessId}/T{ThreadId}) {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            _initialized = true;
            Log.Information("Logger initialized.");
        }
    }

}
