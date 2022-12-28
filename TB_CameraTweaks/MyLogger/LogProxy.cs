using BepInEx.Logging;

namespace TB_CameraTweaks.MyLogger
{
    internal class LogProxy
    {
        //private readonly string _tag = $" [{MyPluginInfo.PLUGIN_NAME}] ";
        private readonly string _tag;

        public LogProxy(string tag = "")
        {
            _tag = tag;
        }

        internal static ManualLogSource _logger { get; set; }

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Fatal level.
        //
        // Parameters: data: Data to log.
        public void LogFatal(object data) => _logger.LogFatal($"{_tag}{data}");

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Error level.
        //
        // Parameters: data: Data to log.
        public void LogError(object data) => _logger.LogError($"{_tag}{data}");

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Warning level.
        //
        // Parameters: data: Data to log.
        public void LogWarning(object data) => _logger.LogWarning($"{_tag}{data}");

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Message level.
        //
        // Parameters: data: Data to log.
        public void LogMessage(object data) => _logger.LogMessage($"{_tag}{data}");

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Info level.
        //
        // Parameters: data: Data to log.
        public void LogInfo(object data) => _logger.LogInfo($"{_tag}{data}");

        public void LogInfoPlain(object data) => _logger.LogInfo(data);

        // Summary: Logs a message with BepInEx.Logging.LogLevel.Debug level.
        //
        // Parameters: data: Data to log.
        internal void LogDebug(object data) => _logger.LogDebug($"(debug) [{_tag}] {data}");
    }
}