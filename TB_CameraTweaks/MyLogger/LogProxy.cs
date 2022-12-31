using BepInEx.Logging;

namespace TB_CameraTweaks.MyLogger
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
        private LogLevel Level;

        public LogProxy(string tag = "", LogLevel level = LogLevel.Info)
        {
            _tag = tag;
            Level = level;
        }

        internal static ManualLogSource _logger { get; set; }

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

        private void SendLogMessage(LogProxyType level, object data)
        {
            if ((int)level > (int)Level) return;

            switch (level)
            {
                case LogProxyType.Plain:
                    if (SkipLogMessage(LogLevel.Info)) return;
                    _logger.LogWarning(data);
                    break;

                case LogProxyType.Debug:
                    if (SkipLogMessage(LogLevel.Debug)) return;
                    _logger.LogWarning($"(debug) [{_tag}] {data}");
                    break;

                case LogProxyType.Message:
                    if (SkipLogMessage(LogLevel.Message)) return;
                    _logger.LogWarning($"{_tag}{data}");
                    break;

                case LogProxyType.Info:
                    if (SkipLogMessage(LogLevel.Info)) return;
                    _logger.LogWarning($"{_tag}{data}");
                    break;

                case LogProxyType.Warning:
                    if (SkipLogMessage(LogLevel.Warning)) return;
                    _logger.LogWarning($"{_tag}{data}");
                    break;

                case LogProxyType.Error:
                    if (SkipLogMessage(LogLevel.Error)) return;
                    _logger.LogError($"{_tag}{data}");
                    break;

                case LogProxyType.Fatal:
                    if (SkipLogMessage(LogLevel.Fatal)) return;
                    _logger.LogMessage($"{_tag}{data}");
                    break;

                default:
                    break;
            }
        }

        private bool SkipLogMessage(LogLevel level) => ((int)level > (int)Level);
    }
}