using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pixeltheory.Blackboard.Modules;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;


namespace Pixeltheory.Messaging
{
    public abstract class PixelSocketBehaviour : PixelBehaviour
    {
        #region Fields
        #region Inspector
        [Header("PixelSocketBehaviour")]
        [SerializeField] protected PixelBlackboardModuleSocketSwitch blackboardModuleSocketSwitch;
        [SerializeField] protected List<PixelKeyValuePair<PixelSocket,UnityAction<Task>>> pixelSocketsIncoming;
        [SerializeField] protected List<PixelSocket> pixelSocketsOutgoing;
        #endregion //Inspector

        #region Private
        [NonSerialized] private int messagingID;
        #endregion //Private
        #endregion //Fields
        
        #region Properties
        #region Public
        public int MessagingID => this.messagingID;
        #endregion //Public
        #endregion //Properties

        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(this.blackboardModuleSocketSwitch);
            this.messagingID = this.GetInstanceID();
        }

        protected void OnEnable()
        {
            foreach (PixelKeyValuePair<PixelSocket,UnityAction<Task>> socketHandler in this.pixelSocketsIncoming)
            {
                //this.blackboardModuleSocketSwitch.RegisterSocket(socketHandler.Key, this.messagingID, socketHandler.Value);
            }
        }

        protected void OnDisable()
        {
            foreach (PixelKeyValuePair<PixelSocket,UnityAction<Task>> socketHandler in this.pixelSocketsIncoming)
            {
                //this.blackboardModuleSocketSwitch.DeregisterSocket(socketHandler.Key, this.messagingID);
            }
        }
        #endregion //Unity Messages
        #endregion //Methods
    }
}
