using System;


namespace Pixeltheory.Messaging
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MessagingTargetAll : Attribute
    {
        /*
         * This attribute is used to specify that a method signature in a messaging interface
         * should send messages to all subscribers of the messaging event. This contrasts with
         * the other messaging interface method signature attribute that specifies that only
         * the targeted object should receive the message.
         */
    }
}