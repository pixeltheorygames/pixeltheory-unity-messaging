using System;

namespace Pixeltheory.Messaging
{
    public abstract class PixelSocketBehaviour : PixelBehaviour
    {
        #region Fields
        #region Protected
        [NonSerialized] private string multicastTypeName;
        [NonSerialized] private int unicastID;
        #endregion //Protected
        #endregion //Fields
        
        #region Properties
        #region Public
        public string MulticastTypeName => this.multicastTypeName;
        public int UnicastID => this.unicastID;
        #endregion //Public
        #endregion //Properties

        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            this.multicastTypeName = this.GetType().Name;
            this.unicastID = this.GetInstanceID();
        }

        protected abstract void OnEnable();
        protected abstract void OnDisable();
        #endregion //Unity Messages
        #endregion //Methods
    }
}
