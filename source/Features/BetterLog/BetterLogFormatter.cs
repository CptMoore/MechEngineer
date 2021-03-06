using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Harmony;
using HBS.Logging;
using UnityEngine;

namespace MechEngineer.Features.BetterLog
{
    public class BetterLogFormatter
    {
        internal static string GetFormattedLogLine(LogLevel logLevel, object message, Exception exception)
        {
            var exSuffix = exception == null ? null : $" Exception: {exception}";
            var line = $"{StartupTime()} [{LogToString(logLevel),-5}] {message}{exSuffix}";

            return line;
        }

        private static string StartupTime()
        {
            var value = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
            return $"{value.Minutes:D2}:{value.Seconds:D2}.{value.Milliseconds:D3}";
        }

        private static string LogToString(LogLevel level)
        {
            return level switch
            {
                LogLevel.Debug => "DEBUG",
                LogLevel.Log => "LOG",
                LogLevel.Warning => "WARN",
                LogLevel.Error => "ERROR",
                _ => "?????"
            };
        }
    }
}