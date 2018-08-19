using log4net;
using OnlineCourses.Domain.Layer.Interfaces;

namespace OnlineCourses.Domain.Layer.Logger {
    public class LoggerWrapper : ILoggerWrapper {
        private readonly ILog _logger;

        public LoggerWrapper() {
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public void Error(string message) {
            _logger.Error(message);
        }

        public void Info(string message) {
            _logger.Info(message);
        }

        public void Warn(string message) {
            _logger.Warn(message);
        }
    }
}
