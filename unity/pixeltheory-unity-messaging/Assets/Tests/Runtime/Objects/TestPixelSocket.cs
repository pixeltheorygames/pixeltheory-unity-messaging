using System.Collections;
using System.Collections.Generic;
using Pixeltheory.Messaging;
using UnityEngine;

namespace Pixeltheory.Messaging.Tests
{
    [CreateAssetMenu(fileName = "TestPixelSocket", menuName = "Pixeltheory/Messaging/Tests/TestPixelSocket")]
    public class TestPixelSocket : PixelSocket
    {
        #region Properties
        #region Public
        public int TestInt { get; set; }
        #endregion //Public
        #endregion //Properties
    }
}