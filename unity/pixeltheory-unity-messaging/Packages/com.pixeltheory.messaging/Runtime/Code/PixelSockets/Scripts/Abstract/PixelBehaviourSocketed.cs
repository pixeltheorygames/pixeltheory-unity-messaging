using UnityEngine;


namespace Pixeltheory.Messaging
{
    public abstract class PixelBehaviourSocketed<TypeSelf, TypeData> : PixelBehaviour<TypeSelf, TypeData>
        where TypeSelf : PixelBehaviourSocketed<TypeSelf, TypeData>
        where TypeData : PixelBlackboard<TypeData>
    {
        #region Properties
        #region Public
        public int UniqueSocketChannel => this.GetInstanceID();
        #endregion //Public
        #endregion //Properties
    }
}
