using System;
using FinLib.Models.Enums;

namespace FinLib.Providers.Logging
{
    public interface IAppLogger
    {
        void Trace(string message);
        void Trace(EventCategory eventCategory, EventId eventId, EventType eventType, string message);
        void Trace(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception);

        void Debug(string message);
        void Debug(EventCategory eventCategory, EventId eventId, EventType eventType, string message);
        void Debug(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception);

        void Info(EventCategory eventCategory, EventId eventId, EventType eventType, string message = null, string customData = null);
        void Info(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, string customData = null);

        void Warn(EventCategory eventCategory, EventId eventId, EventType eventType, string message = null, string customData = null);
        void Warn(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, string customData = null);

        string Error(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception = null, string customData = null);
        string Error(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, Exception exception = null, string customData = null);

        string Fatal(EventCategory eventCategory, EventId eventId, EventType eventType, string message, Exception exception = null, string customData = null);
        string Fatal(EventCategory eventCategory, EventId eventId, EventType eventType, string entityName, string entityTitle, string oldValue, string newValue, string message = null, Exception exception = null, string customData = null);
    }
}
