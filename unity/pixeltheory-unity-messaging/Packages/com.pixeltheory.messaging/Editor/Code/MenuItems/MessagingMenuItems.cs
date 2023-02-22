using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Pixeltheory.Debug;


namespace Pixeltheory.Messaging.Utilities
{
    public class MessagingMenuItems
    {
        #region Class
        #region Methods
        #region Editor Menu Items
        [MenuItem("Pixeltheory/Messaging/Generate Messages Extensions", false, 8001)]
        internal static void GenerateFullMessageExtensionsFile()
        {
            MessagingMenuItems.GenerateMessagingExtensionsFile(false);
        }
        
        [MenuItem("Pixeltheory/Messaging/Generate Stubbed Messages Extensions", false, 8002)]
        internal static void GenerateStubbedMessageExtensionsFile()
        {
            MessagingMenuItems.GenerateMessagingExtensionsFile(true);
        }

        private static void GenerateMessagingExtensionsFile(bool stubbed)
        {
            string messagingExtensionsFileFullpath = null;
            bool messagingExtensionsFileFound = false;
            SerializedObject messagingSettingsSerialized = PixeltheoryMessagingSettings.GetSerializedSettings();
            SerializedProperty messagingExtensionsFileLocation = messagingSettingsSerialized.FindProperty("messagingExtensionsFileLocation");
            if (messagingExtensionsFileLocation != null)
            {
                messagingExtensionsFileFound = File.Exists(messagingExtensionsFileLocation.stringValue);
            }
            if (messagingExtensionsFileFound)
            {
                messagingExtensionsFileFullpath = messagingExtensionsFileLocation.stringValue;
            }
            else
            {
                messagingExtensionsFileFullpath = MessagingUtilitiesEditor.GetSaveAssetPath(MessagingConstants.messagingExtensionsFileName, ".cs");
            }
            if (!String.IsNullOrEmpty(messagingExtensionsFileFullpath) && MessagingExtensionsGenerator.GenerateMessagingExtensions(messagingExtensionsFileFullpath, stubbed))
            {
                messagingExtensionsFileLocation.stringValue = messagingExtensionsFileFullpath;
                messagingSettingsSerialized.ApplyModifiedPropertiesWithoutUndo();
                Logging.Log("[MessagingManager] Successfully generated MessagingExtensions.cs.");
            }
            else
            {
                Logging.Error("[MessagingManager] Error generating MessagingExtensions.cs; check console output.");
            }
        }
        
        [MenuItem("Pixeltheory/Messaging/Generate Control Panel", false, 9001)]
        private static void GenerateControlPanel()
        {
            // string className = nameof(MessagingMenuItems);
            // string controlPanelRelativePath = null;
            // bool filepathFound =
            //     MessagingUtilitiesEditor.GetSaveAssetPath
            //     (
            //         MessagingConstants.controlPanelScriptFilename,
            //         ".cs",
            //         out controlPanelRelativePath
            //     );
            // if (filepathFound)
            // {
            //     List<string> controlPanelNamespaceAndTitle = 
            //         MessagingUtilitiesEditor.GetControlPanelNamespaceAndTitle(controlPanelRelativePath);
            //     if (string.IsNullOrEmpty(controlPanelNamespaceAndTitle[0]) ||
            //         string.IsNullOrEmpty(controlPanelNamespaceAndTitle[1]))
            //     {
            //         Logging.Error("[{0}] Please provide a control panel namespace and title.", className);
            //         return;
            //     }
            //     bool generateControlPanelSucceeded =
            //         MessagingControlPanelGenerator.GenerateControlPanel
            //         (
            //             controlPanelRelativePath,
            //             controlPanelNamespaceAndTitle[0],
            //             controlPanelNamespaceAndTitle[1]
            //         );
            //     if (generateControlPanelSucceeded)
            //     {
            //         Logging.Log("[{0}] Successfully generated MessagingControlPanel.cs.", className);
            //     }
            //     else
            //     {
            //         string generateControlPanelErrorLogFormat =
            //             "[{0}] Error generating MessagingControlPanel.cs; check console output.";
            //         Logging.Error(generateControlPanelErrorLogFormat, className);
            //     }
            // }
            // else
            // {
            //     string fileSelectErrorLogFormat =
            //         "[{0}] Error finding/selecting filepath for MessagingControlPanel.cs; check console output";
            //     Logging.Error(fileSelectErrorLogFormat, className);
            // }
        }
        #endregion //Editor Menu Items
        #endregion //Methods
        #endregion //Class
    }
}