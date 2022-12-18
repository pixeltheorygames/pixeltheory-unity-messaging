using System.Collections.Generic;
using UnityEditor;
using Pixeltheory.Debug;


namespace Pixeltheory.Messaging.Editor
{
    public class MessagingMenuItems
    {
        #region Class
        #region Methods
        #region Editor Menu Items
        [MenuItem("Pixeltheory/Messaging/Generate Messages Extensions", false, 8001)]
        internal static void GenerateMessageExtensionsFile()
        {
            string className = nameof(MessagingMenuItems);
            string eventExtensionsRelativePath = null;
            bool filepathFound =
                MessagingUtilities.GetSaveAssetPath
                (
                    MessagingConstants.messagingExtensionsFileName,
                    ".cs",
                    out eventExtensionsRelativePath
                );
            if (filepathFound)
            {
                if (MessagingExtensionsGenerator.GenerateMessagingExtensions(eventExtensionsRelativePath, false))
                {
                    Logging.Log("[{0}] Successfully generated MessagingExtensions.cs.", className);
                }
                else
                {
                    Logging.Error("[{0}] Error generating MessagingExtensions.cs; check console output.", className);
                }
            }
            else
            {
                string fileSelectErrorLogFormat =
                    "[{0}] Error finding/selecting filepath for MessagingExtensions.cs; check console output";
                Logging.Error(fileSelectErrorLogFormat, className);
            }
        }
        
        [MenuItem("Pixeltheory/Messaging/Generate Stubbed Messages Extensions", false, 8002)]
        internal static void GenerateStubbedMessageExtensionsFile()
        {
            string className = nameof(MessagingMenuItems);
            string eventExtensionsRelativePath = null;
            bool filepathFound =
                MessagingUtilities.GetSaveAssetPath
                (
                    MessagingConstants.messagingExtensionsFileName,
                    ".cs",
                    out eventExtensionsRelativePath
                );
            if (filepathFound)
            {
                if (MessagingExtensionsGenerator.GenerateMessagingExtensions(eventExtensionsRelativePath, true))
                {
                    Logging.Log("[{0}] Successfully generated stubbed MessagingExtensions.cs.", className);
                }
                else
                {
                    string generateExtensionsStubErrorLogFormat =
                        "[{0}] Error generating stubbed MessagingExtensions.cs; check console output.";
                    Logging.Error(generateExtensionsStubErrorLogFormat, className);
                }
            }
            else
            {
                string fileSelectErrorLogFormat =
                    "[{0}] Error finding/selecting filepath for stubbed MessagingExtensions.cs; check console output";
                Logging.Error(fileSelectErrorLogFormat, className);
            }
        }
        
        [MenuItem("Pixeltheory/Messaging/Generate Control Panel", false, 9001)]
        private static void GenerateControlPanel()
        {
            string className = nameof(MessagingMenuItems);
            string controlPanelRelativePath = null;
            bool filepathFound =
                MessagingUtilities.GetSaveAssetPath
                (
                    MessagingConstants.controlPanelScriptFilename,
                    ".cs",
                    out controlPanelRelativePath
                );
            if (filepathFound)
            {
                List<string> controlPanelNamespaceAndTitle = 
                    MessagingUtilities.GetControlPanelNamespaceAndTitle(controlPanelRelativePath);
                if (string.IsNullOrEmpty(controlPanelNamespaceAndTitle[0]) ||
                    string.IsNullOrEmpty(controlPanelNamespaceAndTitle[1]))
                {
                    Logging.Error("[{0}] Please provide a control panel namespace and title.", className);
                    return;
                }
                bool generateControlPanelSucceeded =
                    MessagingControlPanelGenerator.GenerateControlPanel
                    (
                        controlPanelRelativePath,
                        controlPanelNamespaceAndTitle[0],
                        controlPanelNamespaceAndTitle[1]
                    );
                if (generateControlPanelSucceeded)
                {
                    Logging.Log("[{0}] Successfully generated MessagingControlPanel.cs.", className);
                }
                else
                {
                    string generateControlPanelErrorLogFormat =
                        "[{0}] Error generating MessagingControlPanel.cs; check console output.";
                    Logging.Error(generateControlPanelErrorLogFormat, className);
                }
            }
            else
            {
                string fileSelectErrorLogFormat =
                    "[{0}] Error finding/selecting filepath for MessagingControlPanel.cs; check console output";
                Logging.Error(fileSelectErrorLogFormat, className);
            }
        }
        #endregion //Editor Menu Items
        #endregion //Methods
        #endregion //Class
    }
}