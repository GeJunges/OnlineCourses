namespace OnlineCourses.Domain.Layer.Interfaces {
    public interface ILoggerWrapper {
        void Error(string message);
        void Warn(string message);
        void Info(string message);
    }
}
