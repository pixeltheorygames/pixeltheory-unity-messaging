﻿using System;
using Pixeltheory.Blackboard;
using Pixeltheory.Debug;
using UnityEngine;


namespace Pixeltheory.Messaging
{
    public abstract class MessagingBehaviour : PixelBehaviour, IMessageReceiver
    {
        #region Fields
        #region Inspector
        [Header("MessagingBehaviour")]
        [SerializeField] protected MessagingManager messagingManager;
        #endregion //Inspector
        #endregion //Fields
        
        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            // if (!base.IsBeingDestroyed)
            // {
            //     this.messagingManager = 
            //         this.messagingManager == null ? GameObject.FindObjectOfType<MessagingManager>() : this.messagingManager;
            //     if (this.messagingManager != null)
            //     {
            //         this.messagingManager.RegisterForMessages<TypeSelf>(this);
            //     }
            //     else
            //     {
            //         PixelLog.Warn("[{0}] Couldn't find MessagingManager; did not register for messages.", this.name);
            //     }
            // }
        }

        // protected override void OnDestroy()
        // {
        //     // if (this.messagingManager != null)
        //     // {
        //     //     this.messagingManager.DeregisterForMessages<TypeSelf>(this);   
        //     // }
        //     // base.OnDestroy();
        // }
        #endregion //Unity Messages

        #region IMessageReceiver
        public string MessageReceiverName()
        {
            return this.name;
        }

        public int MessageReceiverID()
        {
            return this.GetInstanceID();
        }
        #endregion
        #endregion //Methods
    }
}