using System;
using System.Collections.Generic;
using UnityEngine;


namespace Pixeltheory.Messaging
{
    
    public class MessagingManagerCache : PixelObject
    {
        #region Fields
        #region Insepctor
        [SerializeField] internal Dictionary<string, MessagingManager.MessageKey> messageKeyCacheOffline;
        [SerializeField] internal Dictionary<IMessageReceiver, List<MessagingManager.MessageKey>> messageReceiverCacheOffline;
        #endregion //Inspector
        #endregion //Fields
    }
}