using System;

namespace FFG.Logging {
    public interface ILoggerFactory {
        ILogger CreateLogger(Type type);
    }
}