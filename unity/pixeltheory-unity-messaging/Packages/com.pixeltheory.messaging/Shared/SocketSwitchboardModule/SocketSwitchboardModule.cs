using System.Collections.Generic;
using UnityEngine;
using Pixeltheory.Blackboard;


namespace Pixeltheory.Messaging
{
    [CreateAssetMenu(fileName = "SocketSwitchboardModule", menuName = "Pixeltheory/Blackboard/Modules/SocketSwitchboard")]
    public partial class SocketSwitchboardModule : PixelBlackboardModule
    {
        #region Fields
        #region Inspector
        #if UNITY_EDITOR
        [SerializeField] private List<PixelMessage> pixelMessages;
        #endif //UNITY_EDITOR
        #endregion //Inspector
        #endregion //Fields
    }
}