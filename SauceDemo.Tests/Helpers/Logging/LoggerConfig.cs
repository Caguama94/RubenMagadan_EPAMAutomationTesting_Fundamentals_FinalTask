using Serilog;
using Serilog.Events;

namespace SauceDemo.Tests.Helpers.Logging
{

    public static class LoggerConfig
    {
        private static bool _initialized;

        public static void Init()
        {
            if (_initialized) return;

            // 🔹 Obtener la raíz del proyecto (subiendo desde /bin/Debug/net8.0/)
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var projectRoot = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;

            // 🔹 Construir la ruta deseada: SauceDemo.Tests/Helpers/Logs/Logs-.txt
            var logPath = Path.Combine(projectRoot, "Helpers", "Logs", "Logs-.txt");


            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .WriteTo.File(
                path: logPath,
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
