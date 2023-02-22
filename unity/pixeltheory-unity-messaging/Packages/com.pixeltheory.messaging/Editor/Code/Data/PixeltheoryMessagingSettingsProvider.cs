using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pixeltheory.Messaging;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PixeltheoryMessagingSettingsProvider : SettingsProvider
{
    #region Class
    #region Fields
    class PixeltheoryMessagingSettingsDisplayStyle
    {
        public static GUIContent MessagingExtensionsNamespaceGUIContent = 
            new GUIContent("MessagingExtensions.cs namespace.");
        public static GUIContent ControlPanelNamespaceGUIContent = 
            new GUIContent("MessagingControlPanel.cs namespace.");
        public static GUIContent ControlPanelEditorWindowTitleGUIContent =
            new GUIContent("MessagingControlPanel.cs editor window title.");
    }
    #endregion //Fields
    
    #region Methods
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        string messagingSettingsAssetFullpath =
            PixeltheoryMessagingSettings.PixeltheoryMessagingSettingsAssetDirectory +
            PixeltheoryMessagingSettings.PixeltheoryMessagingSettingsAssetName;
        if (!File.Exists(messagingSettingsAssetFullpath))
        {
            PixeltheoryMessagingSettings.GetOrCreateSettings();
        }
        
        PixeltheoryMessagingSettingsProvider provider = 
            new PixeltheoryMessagingSettingsProvider("Project/Pixeltheory/Messaging", SettingsScope.Project);

        // Automatically extract all keywords from the Styles.
        provider.keywords = SettingsProvider.GetSearchKeywordsFromGUIContentProperties<PixeltheoryMessagingSettingsDisplayStyle>();
        return provider;
    }
    #endregion //Methods
    #endregion //Class
    
    #region Instance
    #region Fields
    #region Private
    private SerializedObject messagingSettings;
    #endregion //Private
    #endregion //Fields
    
    #region Constructor
    public PixeltheoryMessagingSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : 
        base(path, scopes, keywords)
    {
           
    }
    #endregion //Constructor

    #region Methods
    #region SettingsProvider
    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        messagingSettings = PixeltheoryMessagingSettings.GetSerializedSettings();
    }

    public override void OnGUI(string searchContext)
    {
        EditorGUIUtility.labelWidth = 500.0f;
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("MessagingExtensions.cs");
        EditorGUILayout.PropertyField
        (
            messagingSettings.FindProperty("messagingExtensionsNamespace"),
            PixeltheoryMessagingSettingsDisplayStyle.MessagingExtensionsNamespaceGUIContent,
            GUILayout.ExpandWidth(true)
        );
        EditorGUILayout.Space();
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("MessagingControlPanel.cs");
        EditorGUILayout.PropertyField
        (
            messagingSettings.FindProperty("controlPanelNamespace"),
            PixeltheoryMessagingSettingsDisplayStyle.ControlPanelNamespaceGUIContent
        );
        EditorGUILayout.PropertyField
        (
            messagingSettings.FindProperty("controlPanelEditorWindowTitle"),
            PixeltheoryMessagingSettingsDisplayStyle.ControlPanelEditorWindowTitleGUIContent
        );
        messagingSettings.ApplyModifiedPropertiesWithoutUndo();
    }
    #endregion //SettingsProvider
    #endregion //Methods
    #endregion //Instance
}
