using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Pixeltheory.Messaging.Utilities
{
    internal class MessagingControlPanelNamespaceAndTitleInputModal : EditorWindow
    {
        #region Class
        #region Methods
        public static List<string> Show(string title, string inputNamespace, string cpTitle)
        {
            List<string> ret = new List<string>();
            ret.Add("");
            ret.Add("");
            MessagingControlPanelNamespaceAndTitleInputModal window =
                CreateInstance<MessagingControlPanelNamespaceAndTitleInputModal>();
            window.maxScreenPos = GUIUtility.GUIToScreenPoint(new Vector2(Screen.width, Screen.height));
            window.minSize = new Vector2(Screen.width / 4.0f, Screen.height / 10.0f);
            window.titleContent = new GUIContent(title);
            window.messagingControlPanelNamespace = inputNamespace;
            window.messagingControlPanelTitle = cpTitle;
            window.onOKButton += () =>
            {
                ret[0] = window.messagingControlPanelNamespace;
                ret[1] = window.messagingControlPanelTitle;
            };
            window.ShowModal();
            return ret;
        }
        #endregion //Methods
        #endregion //Class

        #region Instance
        #region Fields
        #region Private
        private string messagingControlPanelNamespace = "";
        private string messagingControlPanelTitle = "";
        private bool initializedPosition = false;
        private Action onOKButton;
        private bool shouldClose = false;
        private Vector2 maxScreenPos;
        #endregion //Private
        #endregion //Fields

        #region Methods
        #region Unity Messages
        private void OnGUI()
        {
            // Check if Esc/Return have been pressed
            var e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                switch (e.keyCode)
                {
                    // Escape pressed
                    case KeyCode.Escape:
                        shouldClose = true;
                        e.Use();
                        break;

                    // Enter pressed
                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                        onOKButton?.Invoke();
                        shouldClose = true;
                        e.Use();
                        break;
                }
            }

            if (shouldClose)
            {
                // Close this dialog
                this.Close();
            }

            // Draw our control
            var rect = EditorGUILayout.BeginVertical();

            EditorGUILayout.Space(12);
            EditorGUILayout.LabelField("ENTER DESIRED CONTROL PANEL NAMESPACE AND TITLE");

            EditorGUILayout.Space(8);
            this.messagingControlPanelNamespace =
                EditorGUILayout.TextField("Namespace", this.messagingControlPanelNamespace);
            EditorGUILayout.Space(8);
            this.messagingControlPanelTitle = EditorGUILayout.TextField("Title", this.messagingControlPanelTitle);
            EditorGUILayout.Space(12);

            // Draw OK / Cancel buttons
            Rect controlRect = EditorGUILayout.GetControlRect();
            controlRect.width /= 2;
            if (GUI.Button(controlRect, "OK"))
            {
                this.onOKButton?.Invoke();
                this.shouldClose = true;
            }

            controlRect.x += controlRect.width;
            if (GUI.Button(controlRect, "CANCEL"))
            {
                this.messagingControlPanelNamespace = null; // Cancel - delete inputText
                this.shouldClose = true;
            }

            EditorGUILayout.Space(8);
            EditorGUILayout.EndVertical();

            // Force change size of the window
            if (rect.width != 0 && minSize != rect.size)
            {
                minSize = maxSize = rect.size;
            }

            // Set dialog position next to mouse position
            if (!initializedPosition && e.type == EventType.Layout)
            {
                initializedPosition = true;
                Focus();
            }
        }
        #endregion //Unity Messages
        #endregion //Methods
        #endregion //Instance
    }
}