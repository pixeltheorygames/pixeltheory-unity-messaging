using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Pixeltheory.Debug;


namespace Pixeltheory.Messaging
{
    [DefaultExecutionOrder(MessagingManager.MessagingManagerExecutionOrder)]
    public class MessagingManager : PixelBehaviour<MessagingManager>
    {
        #region Class
        public const int MessagingManagerExecutionOrder = DataManager.DataManagerExecutionOrder + 1;
        #endregion //Class

        #region Instance
        #region Fields
        #region Internal Fields
        internal struct MessageKey
        {
            internal Type type;
            internal string message;
        }
        private Dictionary<MessageKey, List<IMessageReceiver>> messageReceivers;
        private Dictionary<IMessageReceiver, List<MessageKey>> messageKeys;
        private Dictionary<Type, List<MessageKey>> typeMessages;
        #endregion //Local Access Variables
        #endregion //Fields

        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            Logging.Log(this.name + " - Initializing.");
            this.messageReceivers = new Dictionary<MessageKey, List<IMessageReceiver>>();
            this.messageKeys = new Dictionary<IMessageReceiver, List<MessageKey>>();
            this.typeMessages = new Dictionary<Type, List<MessageKey>>();
        }

        protected override void OnDestroy()
        {
            Logging.Log(this.name + " - Shutting down.");
            this.messageReceivers.Clear();
            this.messageKeys.Clear();
            this.typeMessages.Clear();
            base.OnDestroy();
        }
        #endregion //Unity Messages

        #region Local Access Methods
        internal bool RegisterForMessagesFromCache(Type interfaceType, IMessageReceiver listener)
        {
            bool successfullyRegisteredFromCache = false;
            List<MessageKey> cachedMessages;
            this.typeMessages.TryGetValue(interfaceType, out cachedMessages);
            if (cachedMessages != null)
            {
                foreach (MessageKey key in cachedMessages)
                { 
                    this.RegisterForMessagesInternal(key, listener);
                }
                successfullyRegisteredFromCache = true;
            }
            return successfullyRegisteredFromCache;
        }

        internal void AddMessagesToCache(Type interfaceType, List<MessageKey> messages)
        {
            if (this.typeMessages.ContainsKey(interfaceType))
            {
                this.typeMessages.Remove(interfaceType);
            }
            this.typeMessages.Add(interfaceType, messages);
        }

        internal void RegisterForMessagesInternal(MessageKey key, IMessageReceiver listener)
        {
            // Register listener
            List<IMessageReceiver> registeredListeners;
            this.messageReceivers.TryGetValue(key, out registeredListeners);
            if (registeredListeners == null)
            {
                // Add empty list if this is the first time this event has been subscribed to
                registeredListeners = new List<IMessageReceiver>();
                registeredListeners.Add(listener);
                this.messageReceivers.Add(key, registeredListeners);
            }
            else if (!registeredListeners.Contains(listener))
            {
                registeredListeners.Add(listener);
            }
            
            // Add key to registeredKeys if needed
            List<MessageKey> registeredKeys;
            this.messageKeys.TryGetValue(listener, out registeredKeys);
            if (registeredKeys == null)
            {
                // First time this listener has registered for messages so create new list
                registeredKeys = new List<MessageKey>();
                registeredKeys.Add(key);
                this.messageKeys.Add(listener, registeredKeys);
            }
            else if (!registeredKeys.Contains(key))
            {
                registeredKeys.Add(key);
            }
        }

        internal void DeregisterForMessagesInternal(IMessageReceiver listener)
        {
            List<MessageKey> registeredKeys;
            this.messageKeys.TryGetValue(listener, out registeredKeys);
            if (registeredKeys != null)
            {
                foreach (MessageKey keyToRemove in registeredKeys)
                {
                    List<IMessageReceiver> registeredListeners;
                    this.messageReceivers.TryGetValue(keyToRemove, out registeredListeners);
                    if (registeredListeners != null && registeredListeners.Contains(listener))
                    {
                        registeredListeners.Remove(listener);
                    }
                }
            }
        }

        internal List<T> GetRegisteredListenersInternal<T>(string messageName)
        {
            List<IMessageReceiver> registeredListeners;
            MessageKey key = new MessageKey { type = typeof(T), message = messageName };
            this.messageReceivers.TryGetValue(key, out registeredListeners);
            if (registeredListeners == null)
            {
                // Add empty list if this is the first time this event has been fired
                registeredListeners = new List<IMessageReceiver>();
                this.messageReceivers.Add(key, registeredListeners);
            }
            return registeredListeners.Cast<T>().ToList();
        }
        #endregion //Local Access Methods
        #endregion //Methods
        #endregion //Instance
    }

    public static class MessagingManagerCoreExtensions
    {
        public static void RegisterForMessages(this MessagingManager messagingManager, IMessageReceiver listener)
        {
            messagingManager = 
                messagingManager == null ? GameObject.FindObjectOfType<MessagingManager>() : messagingManager;
            if (messagingManager != null)
            {
                Logging.Log(listener.GameObjectName() + " - Registering all messaging methods.");
                Type listenerType = listener.GetType();
                if (!messagingManager.RegisterForMessagesFromCache(listenerType, listener))
                {
                    List<string> listenerMethodNames = new List<string>();
                    MethodInfo[] listenerMethods = listenerType.GetMethods();
                    foreach (MethodInfo method in listenerMethods)
                    {
                        // Filter out all methods which have the MessagingNOP attribute
                        MessagingNOP messagingNOPAttribute = method.GetCustomAttribute<MessagingNOP>();
                        if (messagingNOPAttribute == null)
                        {
                            listenerMethodNames.Add(method.Name);
                        }
                    }

                    // Find all messaging interfaces this component adheres to
                    Type[] listerInterfaces = listenerType.GetInterfaces();
                    List<Type> messagingInterfaces = new List<Type>();
                    foreach (Type potentialInterfaceType in listerInterfaces)
                    {
                        MessagingInterface messagingInterfaceAttribute = potentialInterfaceType.GetCustomAttribute<MessagingInterface>();
                        if (messagingInterfaceAttribute != null)
                        {
                            messagingInterfaces.Add(potentialInterfaceType);
                        }
                    }

                    // Register for messages with the MessagingManager while building cache entry
                    List<MessagingManager.MessageKey> messagesToCache = new List<MessagingManager.MessageKey>();
                    foreach(Type messagingInterfaceType in messagingInterfaces)
                    {
                        MethodInfo[] interfaceMethods = messagingInterfaceType.GetMethods(); 
                        foreach (MethodInfo method in interfaceMethods)
                        { 
                            if (listenerMethodNames.Contains(method.Name))  // We have to match the names of the methods
                            {
                                // Register for message
                                MessagingManager.MessageKey key = new MessagingManager.MessageKey { type = messagingInterfaceType, message = method.Name };
                                messagingManager.RegisterForMessagesInternal(key, listener);
                                messagesToCache.Add(key);
                            }
                        }
                    }

                    messagingManager.AddMessagesToCache(listenerType, messagesToCache);
                }
            }
        }

        public static void DeregisterForMessages(this MessagingManager messagingManager, IMessageReceiver listener)
        {
            messagingManager = 
                messagingManager == null ? GameObject.FindObjectOfType<MessagingManager>() : messagingManager;
            if (messagingManager != null)
            {
                messagingManager.DeregisterForMessagesInternal(listener);
            }
        }

        public static List<T> GetRegisteredListeners<T>(this MessagingManager messagingManager, string messageName)
        {
            if (messagingManager != null)
            {
                return messagingManager.GetRegisteredListenersInternal<T>(messageName);
            }
            else
            {
                // We return an empty list here if the messaging manager does not
                // currently exist. This should only happen if an event is called
                // during boot-up or shutdown, so the impact to garbage collection
                // can be ignored.
                return new List<T>();
            }
        }
    }
}
