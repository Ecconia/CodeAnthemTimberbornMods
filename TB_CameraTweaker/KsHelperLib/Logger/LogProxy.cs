using BepInEx.Logging;

namespace TB_CameraTweaker.KsHelperLib.Logger
{
    internal enum LogProxyType
    {
        Plain,
        Debug,
        Info,
        Message,
        Warning,
        Error,
        Fatal
    }

    internal class LogProxy
    {
        private readonly string _tag;
        public static LogLevel Level { get; set; }
        private readonly LogLevel _level;

        public LogProxy(string tag = "", LogLevel level = LogLevel.All) {
            _tag = tag;
            _level = level;
        }

        internal static ManualLogSource Logger { get; set; }

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Fatal level.
        //
        // Parameters: data: Data to log.
        public void LogFatal(object data) => SendLogMessage(LogProxyType.Fatal, data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Error level.
        //
        // Parameters: data: Data to log.
        public void LogError(object data) => SendLogMessage(LogProxyType.Error, data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Warning level.
        //
        // Parameters: data: Data to log.
        public void LogWarning(object data) => SendLogMessage(LogProxyType.Warning, data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Message level.
        //
        // Parameters: data: Data to log.
        public void LogMessage(object data) => SendLogMessage(LogProxyType.Message, data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Info level.
        //
        // Parameters: data: Data to log.
        public void LogInfo(object data) => SendLogMessage(LogProxyType.Info, data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Info level without tag.
        //
        // Parameters: data: Data to log.
        public void LogInfoPlain(object data) => SendLogMessage(LogProxyType.Plain, data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Debug level.
        //
        // Parameters: data: Data to log.
        internal void LogDebug(object data) => SendLogMessage(LogProxyType.Debug, data);

        private void SendLogMessage(LogProxyType level, object data) {
            //_logger.LogError($"Global Level is: {LogProxy.Level}, Instance Level is: {_level}");
            if ((int)level > (int)Level) return;

            switch (level) {
                case LogProxyType.Plain:
                    if (SkipLogMessage(LogLevel.Info)) return;
                    Logger.LogWarning(data);
                    break;

                case LogProxyType.Debug:
                    if (SkipLogMessage(LogLevel.Debug)) return;
                    Logger.LogWarning($"(debug) [{_tag}] {data}");
                    break;

                case LogProxyType.Message:
                    if (SkipLogMessage(LogLevel.Message)) return;
                    Logger.LogWarning($"{_tag}{data}");
                    break;

                case LogProxyType.Info:
                    if (SkipLogMessage(LogLevel.Info)) return;
                    Logger.LogWarning($"{_tag}{data}");
                    break;

                case LogProxyType.Warning:
                    if (SkipLogMessage(LogLevel.Warning)) return;
                    Logger.LogWarning($"{_tag}{data}");
                    break;

                case LogProxyType.Error:
                    if (SkipLogMessage(LogLevel.Error)) return;
                    Logger.LogError($"{_tag}{data}");
                    break;

                case LogProxyType.Fatal:
                    if (SkipLogMessage(LogLevel.Fatal)) return;
                    Logger.LogMessage($"{_tag}{data}");
                    break;

                default:
                    break;
            }
        }

        private bool SkipLogMessage(LogLevel level) {
            if (Level == LogLevel.All) return (int)level > (int)_level;

            return (int)level > (int)Level;
        }
    }
}