using Pixeltheory.Debug;
using UnityEngine;


namespace Pixeltheory.Messaging
{
    public class MessagingBehaviour<TypeComponent> : PixelBehaviour<TypeComponent>, IMessageReceiver
        where TypeComponent : PixelBehaviour<TypeComponent>
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
            if (!base.isBeingDestroyed)
            {
                this.messagingManager = 
                    this.messagingManager == null ? GameObject.FindObjectOfType<MessagingManager>() : this.messagingManager;
                if (this.messagingManager != null)
                {
                    this.messagingManager.RegisterForMessages(this);
                }
                else
                {
                    Logging.Warn("[{0}] Couldn't find MessagingManager; did not register for messages.", this.name);
                }
            }
        }

        protected override void OnDestroy()
        {
            if (this.messagingManager != null)
            {
                this.messagingManager.DeregisterForMessages(this);   
            }
            base.OnDestroy();
        }
        #endregion //Unity Messages

        #region IMessageReceiver
        public string GameObjectName()
        {
            return this.name;
        }
        #endregion
        #endregion //Methods
    }

    public class MessagingBehaviour<TypeComponent, TypeRuntimeData> : PixelBehaviour<TypeComponent, TypeRuntimeData>, IMessageReceiver
         where TypeComponent : PixelBehaviour<TypeComponent>
         where TypeRuntimeData : PixelRuntimeData<TypeRuntimeData>
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
            if (!base.isBeingDestroyed)
            {
                this.messagingManager = 
                    this.messagingManager == null ? GameObject.FindObjectOfType<MessagingManager>() : this.messagingManager;
                if (this.messagingManager != null)
                {
                    this.messagingManager.RegisterForMessages(this);
                }
                else
                {
                    Logging.Warn("[{0}] Couldn't find MessagingManager; did not register for messages.", this.name);
                }
            }
        }

        protected override void OnDestroy()
        {
            if (this.messagingManager != null)
            {
                this.messagingManager.DeregisterForMessages(this);   
            }
            base.OnDestroy();
        }
        #endregion //Unity Messages

        #region IMessageReceiver
        public string GameObjectName()
        {
            return this.name;
        }
        #endregion
        #endregion //Methods
    }
}