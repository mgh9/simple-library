using FinLib.Common.Exceptions.Base;
using FinLib.Models.Enums;
using NLog;

namespace FinLib.Providers.Logging
{
    public class AppLogger : IAppLogger
    {
        private static readonly ILogger _generalLogger = LogManager.GetCurrentClassLogger();

        public void Trace(string message)
        {
            _generalLogger.Trace(message);
        }
        
        public void Trace(EventCategory eventCategory, EventId eventId, EventType eventType, string message)
        {
            LogEventInfo theEvent = new()
            {
                Message = message,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;

            _generalLogger.Trace(theEvent);
        }

        public void Trace(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception)
        {
            LogEventInfo theEvent = new()
            {
                Message = message,
                Exception = exception
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;

            _generalLogger.Trace(theEvent);
        }
        
        public void Debug(string message)
        {
            _generalLogger.Debug(message);
        }

        public void Debug(EventCategory eventCategory, EventId eventId, EventType eventType, string message)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                Message = message,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;

            _generalLogger.Debug(theEvent);
        }

        public void Debug(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception)
        {
            LogEventInfo theEvent = new()
            {
                Message = message,
                Exception = exception
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;

            _generalLogger.Debug(exception, message);
        }

        public void Info(EventCategory eventCategory, EventId eventId, EventType eventType, string message = null, string customData = null)
        {
            LogEventInfo theEvent = new()
            {
                Message = message,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["custom-data"] = customData;

            _generalLogger.Info(theEvent);
        }

        public void Info(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, string customData = null)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                Message = message,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["entity-name"] = entityName;
            theEvent.Properties["entity-title"] = entityTitle;
            theEvent.Properties["old-value"] = oldValue;
            theEvent.Properties["new-value"] = newValue;
            theEvent.Properties["custom-data"] = customData;

            _generalLogger.Info(theEvent);
        }

        public void Warn(EventCategory eventCategory, EventId eventId, EventType eventType, string message = null, string customData = null)
        {
            LogEventInfo theEvent = new()
            {
                Message = message,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["custom-data"] = customData;

            _generalLogger.Warn(theEvent);
        }

        public void Warn(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, string customData = null)
        {
            LogEventInfo theEvent = new()
            {
                Message = message,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["entity-name"] = entityName;
            theEvent.Properties["entity-title"] = entityTitle;
            theEvent.Properties["old-value"] = oldValue;
            theEvent.Properties["new-value"] = newValue;
            theEvent.Properties["custom-data"] = customData;

            _generalLogger.Warn(theEvent);
        }

        public string Error(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception = null, string customData = null)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                Message = message,
                Exception = exception,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["custom-data"] = customData;

            string traceId;
            if (exception is Common.Exceptions.Base.BaseException aBaseException)
            {
                traceId = aBaseException.TraceId.ToString();
            }
            else
            {
                traceId = Guid.NewGuid().ToString();
            }
            theEvent.Properties["finlib-trace-id"] = traceId;

            _generalLogger.Error(theEvent);

            return traceId;
        }

        public string Error(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, Exception exception = null, string customData = null)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                Message = message,
                Exception= exception,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["entity-name"] = entityName;
            theEvent.Properties["entity-title"] = entityTitle;
            theEvent.Properties["old-value"] = oldValue;
            theEvent.Properties["new-value"] = newValue;
            theEvent.Properties["custom-data"] = customData;

            string traceId;
            if (exception is BaseException anIdpException)
            {
                traceId = anIdpException.TraceId.ToString();
            }
            else
            {
                traceId = Guid.NewGuid().ToString();
            }
            theEvent.Properties["finlib-trace-id"] = traceId;

            _generalLogger.Error(theEvent);

            return traceId;
        }

        public string Fatal(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception = null, string customData = null)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                Message = message,
                Exception = exception,
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["custom-data"] = customData;

            string traceId;
            if (exception is BaseException anIdpException)
            {
                traceId = anIdpException.TraceId.ToString();
            }
            else
            {
                traceId = Guid.NewGuid().ToString();
            }
            theEvent.Properties["finlib-trace-id"] = traceId;

            _generalLogger.Fatal(theEvent);

            return traceId;
        }

        public string Fatal(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, Exception exception = null, string customData = null)
        {
            LogEventInfo theEvent = new LogEventInfo
            {
                Message = message,
                Exception = exception
            };

            theEvent.Properties["category"] = (int)eventCategory;
            theEvent.Properties["event-id"] = (int)eventId;
            theEvent.Properties["event-type"] = (int)eventType;
            theEvent.Properties["entity-name"] = entityName;
            theEvent.Properties["entity-title"] = entityTitle;
            theEvent.Properties["old-value"] = oldValue;
            theEvent.Properties["new-value"] = newValue;
            theEvent.Properties["custom-data"] = customData;

            string traceId;
            if (exception is BaseException aBaseException)
            {
                traceId = aBaseException.TraceId.ToString();
            }
            else
            {
                traceId = Guid.NewGuid().ToString();
            }
            theEvent.Properties["finlib-trace-id"] = traceId;

            _generalLogger.Fatal(theEvent);

            return traceId;
        }
    }
}
