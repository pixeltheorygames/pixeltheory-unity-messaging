using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Pixeltheory.Debug;
using Pixeltheory.Messaging.Utilities;


namespace Pixeltheory.Messaging
{
    [DefaultExecutionOrder(MessagingManager.MessagingManagerExecutionOrder)]
    public class MessagingManager : PixelBehaviour<MessagingManager>
    {
        #region Class
        public const int MessagingManagerExecutionOrder = Int32.MinValue;
        #endregion //Class

        #region Instance
        #region Fields
        #region Inspector
        [Header("MessagingManager")]
        [SerializeField] private MessagingManagerCache offlineCache;
        [SerializeField] private bool writeBackToOfflineCache;
        #endregion //Inspector
        
        #region Internal
        internal enum MessageTargetType
        {
            Unknown,
            MessageTargetAll,
            MessageTargetSingle,
            MessageTargetMulti
        }
        internal struct MessageKey
        {
            internal MessageTargetType MessageTargetType;
            internal Type type;
            internal string messageFullName;
        }
        private Dictionary<string, MessageKey> messageKeyCache;
        private Dictionary<IMessageReceiver, List<MessageKey>> messageReceiverCache;
        private Dictionary<MessageKey, Dictionary<int, List<IMessageReceiver>>> messageReceivers;
        #endregion //Internal
        #endregion //Fields

        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            PixelLog.Log(this.name + " - Initializing.");
            bool offlineCacheObjectExists = this.offlineCache != null;
            this.messageKeyCache = 
                offlineCacheObjectExists && this.offlineCache.messageKeyCacheOffline != null ? 
                    this.offlineCache.messageKeyCacheOffline : 
                    new Dictionary<string, MessageKey>();
            this.messageReceiverCache =
                offlineCacheObjectExists && this.offlineCache.messageReceiverCacheOffline != null ?
                    this.offlineCache.messageReceiverCacheOffline :
                    new Dictionary<IMessageReceiver, List<MessageKey>>();
            this.messageReceivers = new Dictionary<MessageKey, Dictionary<int, List<IMessageReceiver>>>();
        }

        protected override void OnDestroy()
        {
            PixelLog.Log(this.name + " - Shutting down.");
            
            #if UNITY_EDITOR
            if (this.offlineCache != null && this.writeBackToOfflineCache)
            {
                this.offlineCache.messageKeyCacheOffline = this.messageKeyCache;
                this.offlineCache.messageReceiverCacheOffline = this.messageReceiverCache;
            }
            #endif
            
            this.messageKeyCache.Clear();
            this.messageKeyCache = null;
            this.messageReceiverCache.Clear();
            this.messageReceiverCache = null;
            this.messageReceivers.Clear();
            this.messageReceivers = null;
            base.OnDestroy();
        }
        #endregion //Unity Messages

        #region Internal
        internal void RegisterForMessagesInternal<T>(IMessageReceiver listener)
        {
            int listenerID = listener.MessageReceiverID();
            List<MessageKey> listenerMessageKeys;
            Dictionary<int, List<IMessageReceiver>> idToMessageReceiverMapTemp;
            List<IMessageReceiver> messageReceiversListTemp;
            int listenerIDIndexTemp;
            
            if (!this.messageReceiverCache.TryGetValue(listener, out listenerMessageKeys))
            {
                List<Type> implementedMessagingInterfaceTypes;
                List<MethodInfo> implementedMessagingInterfaceMethods;
                List<MessageKey> messagingInterfaceKeys;
                if (!MessagingUtilitiesRuntime.GetImplementedMessagingInterfaceTypes(typeof(T), out implementedMessagingInterfaceTypes))
                {
                    implementedMessagingInterfaceTypes = new List<Type>();
                }
                if (!MessagingUtilitiesRuntime.GetImplementedMessagingMethods(typeof(T), implementedMessagingInterfaceTypes, out implementedMessagingInterfaceMethods))
                {
                    implementedMessagingInterfaceMethods = new List<MethodInfo>();
                }
                if (!MessagingUtilitiesRuntime.CreateMessageKeys(implementedMessagingInterfaceMethods, this.messageKeyCache, out messagingInterfaceKeys))
                {
                    messagingInterfaceKeys = new List<MessageKey>();
                }
                this.messageReceiverCache.Add(listener, messagingInterfaceKeys);
                listenerMessageKeys = messagingInterfaceKeys;
            }
            
            foreach (MessageKey messageKey in listenerMessageKeys)
            {
                if (messageKey.MessageTargetType != MessageTargetType.Unknown)
                {
                    listenerIDIndexTemp = messageKey.MessageTargetType == MessageTargetType.MessageTargetAll ? 0 : listenerID;
                    if (!this.messageReceivers.TryGetValue(messageKey, out idToMessageReceiverMapTemp))
                    {
                        idToMessageReceiverMapTemp = new Dictionary<int, List<IMessageReceiver>>();
                    }
                    if (!idToMessageReceiverMapTemp.TryGetValue(listenerIDIndexTemp, out messageReceiversListTemp))
                    {
                        messageReceiversListTemp = new List<IMessageReceiver>();
                    }
                    if (!messageReceiversListTemp.Contains(listener))
                    {
                        messageReceiversListTemp.Add(listener);
                    }
                    idToMessageReceiverMapTemp[listenerIDIndexTemp] = messageReceiversListTemp;
                    this.messageReceivers[messageKey] = idToMessageReceiverMapTemp;
                }
            }
        }
        
        internal void DeregisterForMessagesInternal<T>(IMessageReceiver listener)
        {
            List<MessageKey> registeredKeys;
            if (this.messageReceiverCache.TryGetValue(listener, out registeredKeys))
            {
                foreach (MessageKey key in registeredKeys)
                {
                    Dictionary<int, List<IMessageReceiver>> targetIDToMessageReceiversMap;
                    if (this.messageReceivers.TryGetValue(key, out targetIDToMessageReceiversMap))
                    {
                        int targetID = key.MessageTargetType == MessageTargetType.MessageTargetSingle ? listener.MessageReceiverID() : 0;
                        List<IMessageReceiver> messageKeyReceivers;
                        if (targetIDToMessageReceiversMap.TryGetValue(targetID, out messageKeyReceivers))
                        {
                            messageKeyReceivers.Remove(listener);
                        }
                    }
                }
                this.messageReceiverCache.Remove(listener);
            }
        }

        // internal bool GetRegisteredMessageListenersInternal(string messageNameFull, int targetObjectID, out List<IMessageReceiver> messageListeners)
        // {
        //     bool result = false;
        //     List<IMessageReceiver> receivers = null;
        //     MessageKey key;
        //     if (this.messageKeyCache.TryGetValue(messageNameFull, out key))
        //     {
        //         Dictionary<int, List<IMessageReceiver>> targetIdToMessageReceiversMap;
        //         if (this.messageReceivers.TryGetValue(key, out targetIdToMessageReceiversMap))
        //         {
        //             if (targetIdToMessageReceiversMap.TryGetValue(targetObjectID, out receivers))
        //             {
        //                 result = true;
        //             }
        //         }
        //     }
        //     messageListeners = receivers;
        //     return result;
        // }
        internal bool GetRegisteredMessageListenersInternal(string messageNameFull, int targetObjectID, out List<IMessageReceiver> messageListeners)
        {
            bool result = false;
            List<IMessageReceiver> receivers = null;
            MessageKey key;
            if (this.messageKeyCache.TryGetValue(messageNameFull, out key))
            {
                Dictionary<int, List<IMessageReceiver>> targetIdToMessageReceiversMap;
                if (this.messageReceivers.TryGetValue(key, out targetIdToMessageReceiversMap))
                {
                    if (targetIdToMessageReceiversMap.TryGetValue(targetObjectID, out receivers))
                    {
                        result = true;
                    }
                }
            }
            messageListeners = receivers;
            return result;
        }
        #endregion //Internal
        #endregion //Methods
        #endregion //Instance
    }

    public static class MessagingManagerCoreExtensions //Public facing MessagingManager API
    {
        public static void RegisterForMessages<T>(this MessagingManager messagingManager, IMessageReceiver listener)
        {
            if (messagingManager != null)
            {
                messagingManager.RegisterForMessagesInternal<T>(listener);
            }
        }

        public static void DeregisterForMessages<T>(this MessagingManager messagingManager, IMessageReceiver listener)
        {
            if (messagingManager != null)
            {
                messagingManager.DeregisterForMessagesInternal<T>(listener);
            }
        }

        public static List<T> GetRegisteredMessageListeners<T>(this MessagingManager messagingManager, string messageNameFull, int targetObjectID)
        {
            List<IMessageReceiver> messageListeners;
            if (messagingManager == null || !messagingManager.GetRegisteredMessageListenersInternal(messageNameFull, targetObjectID, out messageListeners))
            {
                messageListeners = new List<IMessageReceiver>();
            }
            return messageListeners.Cast<T>().ToList();
        }
    }
}
