using System.IO;
using UnityEditor;
using UnityEngine;


namespace Pixeltheory.Messaging
{
    public class PixeltheoryMessagingSettings : PixelScriptableObject
    {
        #region Class
        #region Fields
        #region Public
        public const string PixeltheoryMessagingSettingsAssetDirectory = "Assets/Pixeltheory/Data/Editor/";
        public const string PixeltheoryMessagingSettingsAssetName = "PixeltheoryMessagingSettings.asset";
        #endregion //Public
        #endregion //Fields
        
        #region Methods
        #region Internal
        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(PixeltheoryMessagingSettings.GetOrCreateSettings());
        }

        internal static PixeltheoryMessagingSettings GetOrCreateSettings()
        {
            string pixeltheoryMessagingSettingsAssetFullpath =
                PixeltheoryMessagingSettings.PixeltheoryMessagingSettingsAssetDirectory + 
                PixeltheoryMessagingSettings.PixeltheoryMessagingSettingsAssetName;
            PixeltheoryMessagingSettings settings =
                AssetDatabase.LoadAssetAtPath<PixeltheoryMessagingSettings>(pixeltheoryMessagingSettingsAssetFullpath);
            if (settings == null)
            {
                Directory.CreateDirectory(PixeltheoryMessagingSettings.PixeltheoryMessagingSettingsAssetDirectory);
                settings = PixelScriptableObject.CreateInstance<PixeltheoryMessagingSettings>();
                settings.messagingExtensionsNamespace = "";
                settings.controlPanelNamespace = "";
                settings.controlPanelEditorWindowTitle = "";
                settings.messagingExtensionsFileLocation = "";
                settings.messagingControlPanelFileLocation = "";
                AssetDatabase.CreateAsset(settings, pixeltheoryMessagingSettingsAssetFullpath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }
        #endregion //Internal
        #endregion //Methods
        #endregion //Class

        #region Instance
        #region Fields
        #region Private
        [HideInInspector] [SerializeField] private string messagingExtensionsNamespace;
        [HideInInspector] [SerializeField] private string controlPanelNamespace;
        [HideInInspector] [SerializeField] private string controlPanelEditorWindowTitle;
        [HideInInspector] [SerializeField] internal string messagingExtensionsFileLocation = "";
        [HideInInspector] [SerializeField] internal string messagingControlPanelFileLocation = "";
        #endregion //Private
        #endregion //Fields
        #endregion //Instance
    }
}