using UnityEngine;


namespace Pixeltheory.Messaging
{
    public partial class SocketSwitchboardModule
    {
        #region Fields
        #region Inspector
        [HideInInspector] [SerializeField] private TestIntPixelSocket testIntPixelMessage;
        #endregion //Inspector
        #endregion //Fields

        #region Properties
        #region Public
        public TestIntPixelSocket TestIntPixelSocket => this.testIntPixelMessage;
        #endregion //Public
        #endregion //Properties
    }
    
    [CreateAssetMenu(fileName = "TestIntPixelSocket", menuName = "Pixeltheory/Messaging/Tests/TestIntPixelSocket")]
    public class TestIntPixelSocket : PixelSocket<TestIntPixelSocket>
    {
        #region Fields
        #region Public
        public int TestInt;
        #endregion //Public
        #endregion //Fields
    }
}