
using TaskManagment.Application.Logging;

namespace TaskManagment.Infrastructure.Logging
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger _logger;

        public SerilogLogger(Serilog.ILogger logger)
        {
            _logger = logger;
        }
        public void LogInformation(string message)
        {
            _logger.Information(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }
    }
}
