using UnityEngine;


namespace Pixeltheory.Messaging
{
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
            if (!base.isBeingDestroyed && this.messagingManager == null)
            {
                this.messagingManager = GameObject.FindObjectOfType<MessagingManager>();
                if (this.messagingManager != null)
                {
                    this.messagingManager.RegisterForMessages(this);   
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