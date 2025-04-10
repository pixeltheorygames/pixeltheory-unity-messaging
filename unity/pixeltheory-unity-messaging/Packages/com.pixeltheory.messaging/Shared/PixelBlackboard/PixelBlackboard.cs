using UnityEngine;
using Pixeltheory.Messaging;


namespace Pixeltheory.Blackboard
{
    public partial class PixelBlackboard
    {
        #region Fields
        #region Private
        [SerializeField] private SocketSwitchboardModule socketSwitchboard;
        #endregion //Private
        #endregion //Fields
        
        #region Properties
        #region Public
        public SocketSwitchboardModule Switchboard => this.socketSwitchboard;
        #endregion //Public
        #endregion //Properties
    }
}