using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Pixeltheory.Debug;
using UnityEngine;
using UnityEditor;


namespace Pixeltheory.Messaging.Utilities
{
    public class MessagingControlPanelGenerator
    {
        #region Class
        #region Fields
        private static readonly string[] Indentations = 
            new []
            {
                "", 
                "    ", 
                "        ", 
                "            ", 
                "                ", 
                "                    "
            };
        #endregion //Fields
        
        #region Methods
        #region Internal
        internal static bool GenerateControlPanel(string cpEditorScriptRelativePath, string cpNamespace, string cpTitle)
        {
            string generatorName = nameof(MessagingControlPanelGenerator);
            string cpEditorScriptFilenameFull = MessagingConstants.controlPanelScriptFilename + ".cs";
            if (Application.isPlaying)
            {
                string messageFormat = "[{0}] Editor must be in edit mode to generate {1}.";
                PixelLog.Warn(messageFormat, generatorName, cpEditorScriptFilenameFull);
                return false;
            }
            else
            {
                Type[] messagingInterfaceTypes = MessagingUtilitiesEditor.GetMessagingInterfaceTypes();
                using (StreamWriter outfile = File.CreateText(cpEditorScriptRelativePath))
                {
                    List<string> namespaces = new List<string>();
                    string namespacesBuffer =
                        MessagingControlPanelGenerator.SetupNamespaces
                        (
                            outfile.NewLine,
                            cpEditorScriptFilenameFull,
                            ref namespaces
                        );
                    string fieldsBuffer = MessagingControlPanelGenerator.SetupFieldsRegion(outfile.NewLine);
                    string methodsBuffer = MessagingControlPanelGenerator.SetupMethodsRegion(outfile.NewLine);
                    foreach (Type messageInterfaceType in messagingInterfaceTypes)
                    {
                        MethodInfo[] messageInterfaceMethods = messageInterfaceType.GetMethods();
                        if (messageInterfaceMethods.Length > 0)
                        {
                            fieldsBuffer +=
                                MessagingControlPanelGenerator.SetupInterfaceFoldoutFields
                                (
                                    MessagingControlPanelGenerator.Indentations[2],
                                    messageInterfaceType.Name,
                                    outfile.NewLine
                                );
                            methodsBuffer +=
                                MessagingControlPanelGenerator.SetupInterfaceFoldoutMethods
                                (
                                    MessagingControlPanelGenerator.Indentations[3],
                                    messageInterfaceType.Name,
                                    outfile.NewLine
                                );
                            foreach (MethodInfo messageInterfaceMethod in messageInterfaceMethods)
                            {
                                ParameterInfo[] allParameters = messageInterfaceMethod.GetParameters();
                                List<string> parameterIdentifiers = new List<string>();
                                bool parameterInvalid = false;
                                List<Type> tempTypes = new List<Type>();
                                string tempFields = "";
                                string tempMethods = "";
                                foreach (ParameterInfo parameterInfo in allParameters)
                                {
                                    Type parameterType = parameterInfo.ParameterType;
                                    if (MessagingUtilitiesEditor.ParameterTypeCheck(parameterType))
                                    {
                                        if (!tempTypes.Contains(parameterType))
                                        {
                                            tempTypes.Add(parameterType);
                                        }
                                        string parameterTypeAsString = 
                                            MessagingUtilitiesEditor.GetParameterTypeAsString(parameterType);
                                        string messageParameterIdentifyingName = 
                                            messageInterfaceMethod.Name + parameterInfo.Name;
                                        tempFields += 
                                            MessagingControlPanelGenerator.Indentations[2] +
                                            "[HideInInspector] [SerializeField] private " + 
                                            parameterTypeAsString + 
                                            " " + 
                                            messageParameterIdentifyingName + 
                                            ";" +
                                            outfile.NewLine;
                                        parameterIdentifiers.Add(messageParameterIdentifyingName);
                                        tempMethods += 
                                            MessagingControlPanelGenerator.GetGUIFieldTypeAsString
                                            (
                                                MessagingControlPanelGenerator.Indentations[4],
                                                "serializedEditorWindow",
                                                parameterInfo.Name, 
                                                messageParameterIdentifyingName,
                                                outfile.NewLine 
                                            ) +                 
                                            ";" + 
                                            outfile.NewLine;   
                                    }
                                    else
                                    {
                                        parameterInvalid = true;
                                        break;
                                    }
                                }
                                if (!parameterInvalid)
                                {
                                    foreach (Type parameterType in tempTypes)
                                    {
                                        string parameterNamespace = parameterType.Namespace;
                                        if (!String.IsNullOrEmpty(parameterNamespace))
                                        {
                                            if (!namespaces.Contains(parameterNamespace))
                                            {
                                                namespaces.Add(parameterNamespace);
                                                namespacesBuffer += 
                                                    String.Format("using {0};{1}", parameterNamespace, outfile.NewLine);
                                            }
                                        }
                                    }

                                    fieldsBuffer += tempFields;
                                    methodsBuffer += tempMethods;

                                    methodsBuffer +=
                                        MessagingControlPanelGenerator.GenerateMessageCall
                                        (
                                            MessagingControlPanelGenerator.Indentations[4],
                                            messageInterfaceMethod.Name,
                                            parameterIdentifiers,
                                            outfile.NewLine
                                        );
                                    methodsBuffer += 
                                        MessagingControlPanelGenerator.Indentations[4] + 
                                        "EditorGUILayout.Separator();" + 
                                        outfile.NewLine;
                                }
                                else
                                {
                                    string format =
                                        "[{0}] Message {1} in interface {2} has invalid parameters; skipping.";
                                    PixelLog.Warn
                                    (
                                        format,
                                        generatorName,
                                        messageInterfaceMethod.Name,
                                        messageInterfaceType.Name
                                    );
                                }
                            }
                            methodsBuffer += 
                                MessagingControlPanelGenerator.Indentations[4] + 
                                "EditorGUI.indentLevel--;" + 
                                outfile.NewLine;
                            methodsBuffer += MessagingControlPanelGenerator.Indentations[3] + "}" + outfile.NewLine;
                            methodsBuffer += 
                                MessagingControlPanelGenerator.Indentations[3] + 
                                "EditorGUILayout.EndFoldoutHeaderGroup();" + 
                                outfile.NewLine;
                            methodsBuffer += outfile.NewLine;
                            fieldsBuffer += 
                                MessagingControlPanelGenerator.Indentations[2] +
                                "#endregion //" + 
                                messageInterfaceType.Name + 
                                " Parameters" + 
                                outfile.NewLine;
                            fieldsBuffer += outfile.NewLine;   
                        }
                    }
                    outfile.Write(namespacesBuffer);
                    outfile.WriteLine(outfile.NewLine); //Double line space
                    outfile.Write
                    (
                        MessagingControlPanelGenerator.SetupClass
                        (
                            outfile.NewLine,
                            cpNamespace,
                            MessagingConstants.controlPanelScriptFilename
                        )
                    );
                    outfile.Write
                    (
                        MessagingControlPanelGenerator.SetupClassRegionAndMenuItem
                        (
                            outfile.NewLine,
                            cpNamespace,
                            cpTitle
                        )
                    );
                    outfile.Write(MessagingControlPanelGenerator.CloseClassRegionAndMenuItem(outfile.NewLine));
                    outfile.Write(outfile.NewLine);
                    outfile.Write(MessagingControlPanelGenerator.SetupInstanceRegion(outfile.NewLine));
                    outfile.Write(fieldsBuffer);
                    outfile.Write(MessagingControlPanelGenerator.CloseFieldsRegion(outfile.NewLine));
                    outfile.Write(outfile.NewLine);
                    outfile.Write(methodsBuffer);
                    outfile.Write(MessagingControlPanelGenerator.CloseMethodsRegion(outfile.NewLine));
                    outfile.Write(MessagingControlPanelGenerator.CloseInstanceRegion(outfile.NewLine));
                    outfile.Write(MessagingControlPanelGenerator.CloseClass(outfile.NewLine));
                    outfile.Write(MessagingControlPanelGenerator.CloseNamespaces(outfile.NewLine));
                    outfile.Flush();
                    outfile.Close();
                }

                PixelLog.Log
                (
                    "[{0}] Generated control panel editor script and saved to selected path.",
                    generatorName
                );
                AssetDatabase.Refresh();
                return true;
            }
        }

        private static string SetupNamespaces(string newline, string filename, ref List<string> usedNamespaces)
        {
            string output = "";
            output += "#if UNITY_EDITOR" + newline;
            output += "// " + filename + newline;
            output += "// Auto-Generated " + DateTime.Now + newline;
            output += "using UnityEngine;" + newline;
            usedNamespaces.Add("UnityEngine");
            output += "using UnityEditor;" + newline;
            usedNamespaces.Add("UnityEditor");
            output += "using Pixeltheory;" + newline;
            usedNamespaces.Add("Pixeltheory");
            output += "using Pixeltheory.Debug;" + newline;
            usedNamespaces.Add("Pixeltheory.Debug");
            output += "using Pixeltheory.Messaging;" + newline;
            usedNamespaces.Add("Pixeltheory.Messaging");
            output += "using Pixeltheory.Messaging.Editor;" + newline;
            usedNamespaces.Add( "Pixeltheory.Messaging.Editor" );
            return output;
        }
        
        private static string CloseNamespaces(string newline)
        {
            string output = "";
            output += "#endif //UNITY_EDITOR" + newline;
            return output;
        }

        private static string SetupClass(string newline, string classNamespace, string className)
        {
            string output = "";
            output += newline;
            output += "namespace " + classNamespace + newline;
            output += "{" + newline;
            output += "    public class " + className + " : EditorWindow" + newline;
            output += "    {" + newline;
            return output;
        }

        private static string CloseClass(string newline)
        {
            string output = "";
            output += "    }" + newline;
            output += "}" + newline;
            return output;
        }

        private static string SetupClassRegionAndMenuItem(string newline, string cpNamespace, string cpTitle)
        {
            string output = "";
            output += "        #region Class" + newline;
            output += "        #region Methods" + newline;
            output += "        [MenuItem(\"Pixeltheory/Messaging/";
            output += cpTitle + " Messaging Control Panel\", false, 0)]" + newline;
            output += "        private static void ControlPanelWindow()" + newline;
            output += "        {" + newline;
            output += "            EditorWindow.GetWindow" + newline;
            output += "            (" + newline;
            output += "                typeof(" + cpNamespace + ".MessagingControlPanel" + ")," + newline;
            output += "                false," + newline;
            output += "                " + "\"" + cpTitle + " Messaging Control Panel\"," + newline;
            output += "                true" + newline;
            output += "            ).Show();" + newline;
            output += "            return;" + newline;
            return output;
        }

        private static string CloseClassRegionAndMenuItem(string newline)
        {
            string output = "";
            output += "        }" + newline;
            output += "        #endregion //Methods" + newline;
            output += "        #endregion //Class" + newline;
            return output;
        }

        private static string SetupInstanceRegion(string newline)
        {
            string output = "";
            output += "        #region Instance" + newline;
            return output;
        }
        
        private static string CloseInstanceRegion(string newline)
        {
            string output = "";
            output += "        #endregion //Instance" + newline;
            return output;
        }
        
        private static string SetupFieldsRegion(string newline)
        {
            string output = "";
            output += "        #region Fields" + newline;
            output += "        #region Private" + newline;
            output += "        #region Editor Window" + newline;
            output += "        private Vector2 guiScrollPosition = Vector2.zero;" + newline;
            output += "        #endregion //Editor Window" + newline;
            output += newline;
            output += "        #region Message Parameters" + newline;
            return output;
        }

        private static string CloseFieldsRegion(string newline)
        {
            string output = "";
            output += "        #endregion //Message Parameters" + newline;
            output += "        #endregion //Private" + newline;
            output += "        #endregion //Fields" + newline;
            return output;
        }
        
        private static string SetupMethodsRegion(string newline)
        {
            string output = "";
            output += "        #region Methods" + newline;
            output += "        #region Unity Messages" + newline;
            output += "        private void OnEnable()" + newline;
            output += "        {" + newline;
            output += "            this.guiScrollPosition = Vector2.zero;" + newline; 
            output += "        }" + newline;
            output += newline;
            output += "        private void OnGUI()" + newline;
            output += "        {" + newline;
            output +=
                "            SerializedObject serializedEditorWindow = new SerializedObject(this as ScriptableObject);" + newline;
            output += 
                "            this.guiScrollPosition = EditorGUILayout.BeginScrollView(this.guiScrollPosition);" + newline;
            output += "            MessagingManager messagingManager = GameObject.FindObjectOfType<MessagingManager>();" + newline;
            output += newline;
            return output;
        }

        private static string CloseMethodsRegion(string newline)
        {
            string output = "";
            output += "            EditorGUILayout.EndScrollView();" + newline;
            output += "            serializedEditorWindow.ApplyModifiedProperties();" + newline;
            output += "        }" + newline;
            output += "        #endregion //Unity Messages" + newline;
            output += "        #endregion //Methods" + newline;
            return output;
        }

        private static string SetupInterfaceFoldoutFields(string tabs, string interfaceTypeName, string newline)
        {
            string output = "";
            output += tabs + "#region " + interfaceTypeName + " Parameters" + newline;
            output += tabs + "private bool foldoutToggle" + interfaceTypeName + " = false;";
            output += newline;
            return output;
        }

        private static string SetupInterfaceFoldoutMethods(string tabs, string interfaceTypeName, string newline)
        {
            string output = 
                "{0}this.foldoutToggle{1} = EditorGUILayout.BeginFoldoutHeaderGroup(this.foldoutToggle{1}, \"{1}\");{2}";
            string extraIndentation = MessagingControlPanelGenerator.Indentations[1];
            output += "{0}if(this.foldoutToggle{1}){2}";
            output += "{0}{{{2}";
            output += "{0}" + extraIndentation + "EditorGUILayout.Space();{2}";
            output += "{0}" + extraIndentation + "EditorGUI.indentLevel++;{2}";
            output += "{0}" + extraIndentation + "EditorGUILayout.HelpBox(\"{1}\", MessageType.Info);{2}";
            output = String.Format(output, tabs, interfaceTypeName, newline);
            return output;
        }

        private static string GenerateMessageCall(string indent, string eventName, List<string> parameters, string newline)
        {
            string extraIndentation = MessagingControlPanelGenerator.Indentations[1];
            // {0} indentation
            // {1} extraIndentation
            // {2} newline
            // {3} messageEventName
            // {4} MessagingConstants.controlPanelScriptFilename
            string output = "{0}EditorGUILayout.BeginHorizontal();{2}";
            output += "{0}GUILayout.Space(EditorGUI.indentLevel * MessagingConstants.controlPanelButtonsOffset);{2}";
            output += "{0}if (Application.isPlaying & GUILayout.Button(\"Fire {3} Event \")){2}";
            output += "{0}{{{2}";
            output += "{0}{1}if (messagingManager != null){2}";
            output += "{0}{1}{{{2}";
            output += "{0}{1}{1}messagingManager.{3}Event(";
            for (int index = 0; index < parameters.Count; index++)
            {
                output += "this." + parameters[index];
                if (index < parameters.Count - 1)
                {
                    output += ",";
                }
            }
            output += ");{2}";
            output += "{0}{1}}}{2}";
            output += "{0}{1}else{2}";
            output += "{0}{1}{{{2}";
            output += "{0}{1}{1}Logging.Warn{2}";
            output += "{0}{1}{1}({2}";
            output += 
                "{0}{1}{1}{1}\"[{{0}}] MessagingManager must exist in the scene to fire message from control panel!\",{2}";
            output += "{0}{1}{1}{1}nameof({4}){2}";
            output += "{0}{1}{1});{2}";
            output += "{0}{1}}}{2}";
            output += "{0}}}{2}";
            output += "{0}EditorGUILayout.EndHorizontal();{2}";
            output = String.Format
            (
                output,
                indent,
                extraIndentation,
                newline,
                eventName,
                MessagingConstants.controlPanelScriptFilename
            );
            return output;
        }

        internal static string GetGUIFieldTypeAsString
        (
            string indentation,
            string serializedEditorWindow,
            string parameterName, 
            string messageParameterIdentifyingName,
            string newline
        )
        {
            string output = indentation + "SerializedProperty serialized_{0} = {1}.FindProperty(\"{0}\");" + newline;
            output += indentation + "EditorGUILayout.PropertyField(serialized_{0}, new GUIContent(\"{2}\"), true);";
            output = String.Format(output, messageParameterIdentifyingName, serializedEditorWindow, parameterName);
            return output;
        }
        #endregion //Internal Methods
        #endregion //Methods
        #endregion //Class
    }
}