using System;
using UnityEngine;


namespace Pixeltheory.Messaging
{
    [Serializable]
    public class PixelMessage
    {
        #region Fields
        #region Inspector
        [SerializeReference] private PixelSocket pixelSocket;
        [SerializeField] private bool parallelFlag;
        [SerializeField] private string multicastTypeName;
        [SerializeField] private int unicastID;
        #endregion //Inspector
        #endregion //Fields
    }
}