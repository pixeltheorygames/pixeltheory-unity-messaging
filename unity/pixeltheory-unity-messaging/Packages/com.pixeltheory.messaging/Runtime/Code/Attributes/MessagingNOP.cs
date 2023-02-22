using System;


namespace Pixeltheory.Messaging
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MessagingNOP : Attribute
    {
        // We only use this attribute for tagging methods
        // as not implemented messageFullName handlers.
    }
}