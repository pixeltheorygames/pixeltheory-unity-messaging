using System;


namespace Pixeltheory.Messaging
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public class MessagingInterface : Attribute
    {
        // We only use this attribute for tagging interfaces
        // as messaging handler interfaces.
    }
}