using System;


namespace Pixeltheory.Messaging
{
    [Serializable]
    public struct MessagingKey
    {
        public Type listenerType;
        public string listenerMessageName;
    }
}