using Pixeltheory.Blackboard;
using Pixeltheory.Debug;


namespace Pixeltheory.Messaging
{
    public abstract class PixelBehaviourSocketed<BlackboardDataType> : PixelBehaviour<BlackboardDataType>
        where BlackboardDataType : PixelBlackboardData
    {
        #region Properties
        #region Public
        public int UniqueBehaviourSocketChannel => this.GetInstanceID();
        #endregion //Public
        #endregion //Properties
    }
}
