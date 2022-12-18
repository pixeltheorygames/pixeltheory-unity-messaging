#if UNITY_EDITOR
namespace Pixeltheory.Messaging.Editor
{
    public class MessagingConstants
    { 
        #region Static
        #region Fields
	    #region Public
	    public static float controlPanelButtonsOffset = 18.0f;
	    #endregion //Public

	    #region Internal
        internal static string defaultAssetsPath = "Assets";
        internal static string messagingExtensionsFileName = "MessagingExtensions";
        internal static string controlPanelScriptFilename = "MessagingControlPanel";
        #endregion //Internal
        #endregion //Fields
        #endregion //Static
    }
}
#endif //UNITY_EDITOR
