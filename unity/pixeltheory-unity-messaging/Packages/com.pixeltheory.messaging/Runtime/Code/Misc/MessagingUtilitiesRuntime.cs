using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Utilities;
using MessageKey = Pixeltheory.Messaging.MessagingManager.MessageKey;


namespace Pixeltheory.Messaging.Utilities
{
    public class MessagingUtilitiesRuntime
    {
        #region Class
        #region Methods
        #region Internal
        internal static bool GetImplementedMessagingInterfaceTypes(Type componentType, out List<Type> implementedInterfaceTypes)
        {
            List<Type> messagingInterfaces = new List<Type>();
            Type[] interfaces = componentType.GetInterfaces();
            foreach (Type interfaceType in interfaces)
            {
                if (CustomAttributeExtensions.GetCustomAttribute<MessagingInterface>(interfaceType) != null)
                {
                    messagingInterfaces.Add(interfaceType);
                }
            }
            bool result = messagingInterfaces.Count > 0;
            implementedInterfaceTypes = result ? messagingInterfaces : null;
            return result;
        }
        
        internal static bool GetImplementedMessagingMethods(Type componentType, List<Type> implementedInterfaces, out List<MethodInfo> implementedMessagingInterfaceMethods)
        {
            List<MethodInfo> filteredImplementedMessagingInterfaceMethods = new List<MethodInfo>();
            foreach (Type interfaceType in implementedInterfaces)
            {
                InterfaceMapping map = componentType.GetInterfaceMap(interfaceType);
                for (int i = 0; i < map.TargetMethods.Length; i++)
                {
                    MethodInfo possibleImplementedMethod = map.TargetMethods[i];
                    if (possibleImplementedMethod.GetCustomAttribute<MessagingNOP>() == null)
                    {
                        filteredImplementedMessagingInterfaceMethods.Add(map.InterfaceMethods[i]);
                    }
                }
            }
            bool result = filteredImplementedMessagingInterfaceMethods.Count > 0;
            implementedMessagingInterfaceMethods = result ? filteredImplementedMessagingInterfaceMethods : null;
            return result;
        }
        
        internal static bool CreateMessageKeys(List<MethodInfo> implementedInterfaceMethods, Dictionary<string, MessageKey> messageKeysCache, out List<MessageKey> messageKeys)
        {
            List<MessageKey> generatedMessageKeys = new List<MessageKey>();
            foreach (MethodInfo methodInfo in implementedInterfaceMethods)
            {
                string methodSignatureFull = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;
                MessageKey key;
                if (!messageKeysCache.TryGetValue(methodSignatureFull, out key))
                {
                    key = new MessageKey();
                    key.type = methodInfo.DeclaringType;
                    key.messageFullName = key.type.FullName + "." + methodInfo.Name;
                    key.MessageTargetType = methodInfo.GetCustomAttribute<MessagingTargetSingle>() != null ? MessagingManager.MessageTargetType.MessageTargetSingle : MessagingManager.MessageTargetType.Unknown;
                    key.MessageTargetType = methodInfo.GetCustomAttribute<MessagingTargetAll>() != null ? MessagingManager.MessageTargetType.MessageTargetAll : key.MessageTargetType;
                    messageKeysCache.Add(methodSignatureFull, key);
                }
                generatedMessageKeys.Add(key);
            }
            bool result = generatedMessageKeys.Count > 0;
            messageKeys = result ? generatedMessageKeys : null;
            return result;
        }
        #endregion //Internal
        #endregion //Methods
        #endregion //Class
    }
}