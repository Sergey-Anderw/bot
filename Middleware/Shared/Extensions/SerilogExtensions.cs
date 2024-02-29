using Serilog;
using Serilog.Events;
using Shared.Validation;

namespace Shared.Extensions
{
    public static class SerilogExtensions
    {
        public static void InformationEvent(this ILogger logger, string eventId, string messageTemplate, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Information))
                return;

            LogEvent(logger.Information, eventId, messageTemplate, propertyValues);
        }

        public static void InformationEvent(this ILogger logger, string eventId, string messageTemplate, Exception exception, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Information))
                return;

            LogEvent(logger.Information, exception, eventId, messageTemplate, propertyValues);
        }

        public static void WarningEvent(this ILogger logger, string eventId, string messageTemplate, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Warning))
                return;

            LogEvent(logger.Warning, eventId, messageTemplate, propertyValues);
        }

        public static void WarningEvent(this ILogger logger, string eventId, string messageTemplate, Exception exception, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Warning))
                return;

            LogEvent(logger.Warning, exception, eventId, messageTemplate, propertyValues);
        }

        public static void ErrorEvent(this ILogger logger, string eventId, string messageTemplate, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Error))
                return;

            LogEvent(logger.Error, eventId, messageTemplate, propertyValues);
        }

        public static void ErrorEvent(this ILogger logger, string eventId, string messageTemplate, Exception exception, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Error))
                return;

            LogEvent(logger.Error, exception, eventId, messageTemplate, propertyValues);
        }

        public static void DebugEvent(this ILogger logger, string eventId, string messageTemplate, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Debug))
                return;

            LogEvent(logger.Debug, eventId, messageTemplate, propertyValues);
        }

        public static void DebugEvent(this ILogger logger, string eventId, string messageTemplate, Exception exception, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Debug))
                return;

            LogEvent(logger.Debug, exception, eventId, messageTemplate, propertyValues);
        }

        public static void VerboseEvent(this ILogger logger, string eventId, string messageTemplate, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Verbose))
                return;

            LogEvent(logger.Verbose, eventId, messageTemplate, propertyValues);
        }

        public static void VerboseEvent(this ILogger logger, string eventId, string messageTemplate, Exception exception, params object[] propertyValues)
        {
            Assume.NotNull(logger, nameof(logger));

            if (!logger.IsEnabled(LogEventLevel.Verbose))
                return;

            LogEvent(logger.Verbose, exception, eventId, messageTemplate, propertyValues);
        }

        public static ILogger With(this ILogger logger, string propertyName, object value, bool destructureObjects = true)
        {
            Assume.NotNull(logger, nameof(logger));

            return logger.ForContext(propertyName, value, destructureObjects);
        }

        private static void LogEvent(Action<string, object[]> logAction, string eventId, string messageTemplate, params object[] propertyValues)
        {
            var props = new object[]
                {
                    eventId
                };
            if (propertyValues.Length > 0)
            {
                props = props.Concat(propertyValues)
                    .ToArray();
            }

            var template = "<{EventId:l}>";
            if (!string.IsNullOrWhiteSpace(messageTemplate))
                template += " " + messageTemplate;

            logAction(template, props);
        }

        private static void LogEvent(Action<Exception, string, object[]> logAction, Exception exception, string eventId, string messageTemplate, params object[] propertyValues)
        {
            var props = new object[]
                {
                    eventId
                };
            if (propertyValues.Length > 0)
            {
                props = props.Concat(propertyValues)
                    .ToArray();
            }

            var template = "<{EventId:l}>";
            if (!string.IsNullOrWhiteSpace(messageTemplate))
                template += " " + messageTemplate;

            logAction(exception, template, props);
        }
    }
}