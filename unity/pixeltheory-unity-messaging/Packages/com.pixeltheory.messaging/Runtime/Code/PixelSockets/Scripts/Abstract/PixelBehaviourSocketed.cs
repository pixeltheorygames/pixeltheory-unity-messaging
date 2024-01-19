using Pixeltheory.Debug;


namespace Pixeltheory.Messaging
{
    public abstract class PixelBehaviourSocketed<TypeBlackboard, TypeData> : PixelBehaviour<TypeBlackboard, TypeData>
        where TypeBlackboard : PixelBlackboard<TypeData>
        where TypeData : PixelObject
    {
        #region Properties
        #region Public
        public int UniqueBehaviourSocketChannel => this.GetInstanceID();
        #endregion //Public
        #endregion //Properties
    }
}
