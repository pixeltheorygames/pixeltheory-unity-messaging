using System;


namespace Pixeltheory.Messaging
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MessagingTargetMultiple : Attribute
    {
        /*
         * This attribute is used to specify that a method signature in a messaging interface
         * should send messages to a subset of subscribers of the messaging event. This contrasts with
         * the other messaging interface method signature attributes that specifies that only a single or 
         * all targeted object should receive the message.
         */
    }
}