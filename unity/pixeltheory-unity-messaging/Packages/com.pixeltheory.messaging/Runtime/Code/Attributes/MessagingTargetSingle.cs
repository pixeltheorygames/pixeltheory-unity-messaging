using System;


namespace Pixeltheory.Messaging
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class MessagingTargetSingle : Attribute
    {
        /*
         * This attribute tags messageFullName signatures in messaging interfaces
         * as a selection messageFullName. This means that the generated messageFullName
         * event will have an extra parameter, int ID, which is used to target
         * the messageFullName to the matching Component/GameObject only.
         *
         * If an ID match is not found, the messageFullName will not be delivered.
         *
         * As mentioned above, this attribute is valid only on method signatures
         * in messaging interfaces.
         */
    }
}