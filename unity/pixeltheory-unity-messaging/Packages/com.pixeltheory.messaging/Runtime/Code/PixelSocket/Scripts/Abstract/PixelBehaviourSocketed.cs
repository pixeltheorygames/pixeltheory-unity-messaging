using UnityEngine;


namespace Pixeltheory.Messaging
{
    public abstract class PixelBehaviourSocketed<TypeSelf, TypeData> : PixelBehaviour<TypeSelf, TypeData>
        where TypeSelf : PixelBehaviourSocketed<TypeSelf, TypeData>
        where TypeData : PixelBlackboard<TypeData>
    {
        #region Fields
        #region Private
        private int uniqueSocketChannel;
        #endregion //Private
        #endregion //Fields

        #region Properties
        #region Public
        public int UniqueSocketChannel => this.uniqueSocketChannel;
        #endregion //Public
        #endregion //Properties

        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            this.uniqueSocketChannel = this.GetInstanceID();
        }
        #endregion //Unity Messages
        #endregion //Methods
    }
}