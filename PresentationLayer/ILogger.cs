using System;

namespace PresentationLayer
{
    public interface ILogger
    {
        void Write(LogLevel logLevel, string message);
        void Write(LogLevel logLevel, Exception ex, string message);
    }
}
