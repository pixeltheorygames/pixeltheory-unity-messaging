using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using Pixeltheory.Debug;

using Assembly = System.Reflection.Assembly;
using UnityAssembly = UnityEditor.Compilation.Assembly;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;


namespace Pixeltheory.Messaging.Editor
{
    public class MessagingUtilities
    {
        #region Class
        #region Methods
        #region Internal
        internal static bool GetSaveAssetPath(string filename, string fileExtension, out string path)
        {
            string className = nameof(MessagingUtilities);
            string[] searchFolders = new[] { MessagingConstants.defaultAssetsPath };
            char[] directorySeparators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            string[] guidSearchResults = 
                AssetDatabase.FindAssets(filename, searchFolders);
            string filenameFull = filename + fileExtension;
            foreach (string guid in guidSearchResults)
            {
                string filepath = AssetDatabase.GUIDToAssetPath(guid);
                string[] splitFoundPath = filepath.Split(directorySeparators);
                foreach (string pathPiece in splitFoundPath)
                {
                    if (pathPiece == filenameFull)
                    {
                        string fileFoundLogFormat = "[{0}] Existing {1} found; will overwrite.";
                        Logging.Log(fileFoundLogFormat, className, filenameFull);
                        path = filepath;
                        return true;
                    }
                }
            }
            string saveFolderPanelTitle = "Folder to save " + filenameFull + " to";
            string saveFileSelectedPath =
                EditorUtility.SaveFolderPanel
                (
                    saveFolderPanelTitle,
                    MessagingConstants.defaultAssetsPath,
                    ""
                );
            if (saveFileSelectedPath == string.Empty)
            {
                // User cancelled out of selecting a location
                Logging.Warn("[{0}] No location selected for {1}; exiting.", className, filenameFull);
                path = null;
                return false;
            }
            string[] splitSelectedPath = saveFileSelectedPath.Split(directorySeparators);
            string selectedRelativePath = null;
            bool foundAssetSubfolder = false;
            for (int i = 0; i < splitSelectedPath.Length; i++)
            {
                if (foundAssetSubfolder)
                {
                    selectedRelativePath += splitSelectedPath[i] + "/";
                }
                else if (splitSelectedPath[i] == "Assets")
                {
                    foundAssetSubfolder = true;
                    selectedRelativePath = splitSelectedPath[i] + "/";
                }
            }
            if (foundAssetSubfolder)
            {
                string userSelectedPathFormat =
                    "[{0}] Saving {1} at user selected location at {2}.";
                Logging.Log(userSelectedPathFormat, className, filenameFull, selectedRelativePath);
                selectedRelativePath += filenameFull;
                path = selectedRelativePath;
                return true;
            }
            else
            {
                string userSelectedPathNotFoundFormat =
                    "[{0}] Please select a path to save {1} to inside the Assets subfolder.";
                Logging.Log(userSelectedPathNotFoundFormat, className, filenameFull);
                path = null;
                return false;
            }
        }

        internal static List<string> GetControlPanelNamespaceAndTitle(string controlPanelScriptFilepath)
        {
            List<string> controlPanelNamespaceAndTitle = new List<string>();
            controlPanelNamespaceAndTitle.Add("");
            controlPanelNamespaceAndTitle.Add("");
            if (File.Exists(controlPanelScriptFilepath))
            {
                //Previous control panel script exists, extract namespace
                Assembly[] allAssemblies = System.AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in allAssemblies)
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (type.Name == MessagingConstants.controlPanelScriptFilename)
                        {
                            controlPanelNamespaceAndTitle[0] = type.Namespace;
                            MethodInfo controlPanelWindowMethod =
                                type.GetMethod
                                (
                                    "ControlPanelWindow",
                                    BindingFlags.Static | BindingFlags.NonPublic
                                );
                            MenuItem menuItemAttribute = controlPanelWindowMethod.GetCustomAttribute<MenuItem>();
                            string[] menuItemPieces = menuItemAttribute.menuItem.Split('/');
                            string title = menuItemPieces[menuItemPieces.Length - 1];
                            title = title.Replace("Messaging Control Panel", "");
                            title = title.Trim();
                            controlPanelNamespaceAndTitle[1] = title;
                            break;
                        }
                    }
                }
            }
            else
            {
                //Ask for new namespace from user
                string modalTitle = "ENTER DESIRED CONTROL PANEL NAMESPACE AND TITLE";
                controlPanelNamespaceAndTitle =
                    MessagingControlPanelNamespaceAndTitleInputModal.Show(modalTitle, "", "");
            }
            return controlPanelNamespaceAndTitle;
        }
        
        internal static Type[] GetMessagingInterfaceTypes()
        {
            Assembly[] allAssemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            UnityAssembly[] playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.Player);
            List<Assembly> playerFilteredAssemblies = new List<Assembly>();
            ListRequest packageListRequest = Client.List();
            List<Assembly> packageFilteredAssemblies = new List<Assembly>();
            List<Type> interfaceTypes = new List<Type>();
            // Interleave some of the player assembly filtering with waiting for the package list request
            int playerAssemblyIndex = 0;
            while (!packageListRequest.IsCompleted)
            {
                if (playerAssemblyIndex < playerAssemblies.Length)
                {
                    foreach (Assembly assembly in allAssemblies)
                    {
                        if (assembly.GetName().Name == playerAssemblies[playerAssemblyIndex].name)
                        {
                            playerFilteredAssemblies.Add(assembly);
                            playerAssemblyIndex++;
                            break;
                        }
                    }
                }
            }
            // Finish up player assembly filtering if we need to
            for (int i = playerAssemblyIndex; i < playerAssemblies.Length; i++)
            {
                foreach (Assembly assembly in allAssemblies)
                {
                    if (assembly.GetName().Name == playerAssemblies[playerAssemblyIndex].name)
                    {
                        playerFilteredAssemblies.Add(assembly);
                        break;
                    }
                }
            }
            if (packageListRequest.Error != null)
            {
                Logging.Error("[MessagingEventsAutoGenerator.GetMessagingInterfaceTypes] Package list request returned an error.");
            }
            else
            {
                foreach (Assembly assembly in playerFilteredAssemblies)
                {
                    if (PackageInfo.FindForAssembly(assembly) == null)
                    {
                        packageFilteredAssemblies.Add(assembly);
                    }
                }
            }
            foreach (Assembly assembly in packageFilteredAssemblies)
            {
                Type[] assemblyTypes = assembly.GetTypes();
                foreach (Type potentialType in assemblyTypes)
                {
                    if (potentialType.IsInterface)
                    {
                        MessagingInterface messagingInterfaceAttribute = potentialType.GetCustomAttribute<MessagingInterface>();
                        if (messagingInterfaceAttribute != null)
                        {
                            interfaceTypes.Add(potentialType);
                        }   
                    }
                }
            }
            return interfaceTypes.ToArray();
        }

        internal static bool ParameterTypeCheck(Type type)
        {
            bool parameterTypeIsSupported = false;
            
            parameterTypeIsSupported |= type == typeof(Boolean);
            parameterTypeIsSupported |= type == typeof(Byte);
            parameterTypeIsSupported |= type == typeof(SByte);
            parameterTypeIsSupported |= type == typeof(Single);
            parameterTypeIsSupported |= type == typeof(Double);
            parameterTypeIsSupported |= type == typeof(Int16);
            parameterTypeIsSupported |= type == typeof(UInt16);
            parameterTypeIsSupported |= type == typeof(Int32);
            parameterTypeIsSupported |= type == typeof(UInt32);
            parameterTypeIsSupported |= type == typeof(Int64);
            parameterTypeIsSupported |= type == typeof(UInt64);
            parameterTypeIsSupported |= type == typeof(String);
            parameterTypeIsSupported |= type == typeof(Char);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Vector2);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Vector2Int);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Vector3);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Vector3Int);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Vector4);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Rect);
            parameterTypeIsSupported |= type == typeof(UnityEngine.RectInt);
            parameterTypeIsSupported |= type == typeof(UnityEngine.RectOffset);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Quaternion);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Color);
            parameterTypeIsSupported |= type == typeof(UnityEngine.LayerMask);
            parameterTypeIsSupported |= type == typeof(UnityEngine.AnimationCurve);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Gradient);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Bounds);
            parameterTypeIsSupported |= type == typeof(UnityEngine.BoundsInt);
            parameterTypeIsSupported |= (type.IsEnum && type.IsSerializable);
            parameterTypeIsSupported |= (type.IsValueType && type.IsSerializable && !type.IsEnum);
            parameterTypeIsSupported |= 
                (type.IsClass && type.IsSerializable && !type.IsAbstract && !type.IsGenericType);
            parameterTypeIsSupported |= type == typeof(UnityEngine.Object);
            parameterTypeIsSupported |= 
                (type.IsSubclassOf(typeof(UnityEngine.Object)) && !type.IsAbstract && !type.IsGenericType);
            parameterTypeIsSupported |= type == typeof(UnityEngine.GameObject);
            parameterTypeIsSupported |= 
                (type.IsSubclassOf(typeof(UnityEngine.GameObject)) && !type.IsAbstract && !type.IsGenericType);
            parameterTypeIsSupported |= type == typeof(UnityEngine.ScriptableObject);
            parameterTypeIsSupported |= 
                (type.IsSubclassOf(typeof(UnityEngine.ScriptableObject)) && !type.IsAbstract && !type.IsGenericType);
            parameterTypeIsSupported |= type.IsArray && ParameterTypeCheck(type.GetElementType());
            parameterTypeIsSupported |= (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
            
            return parameterTypeIsSupported;
        }

        internal static string GetParameterTypeAsString(Type type)
        {
            string typeAsString = type.Name;
            
            if (type.IsNested)
            {
                if (typeAsString.Contains("+"))
                {
                    typeAsString = typeAsString.ToString().Replace("+", ".");
                }
                else if (type.DeclaringType != null)
                {
                    typeAsString = type.DeclaringType.Name + "." + typeAsString;
                }
            }
            if (type.IsGenericType)
            {
                typeAsString = typeAsString.Replace("`1", "<");
                foreach (Type genericType in type.GenericTypeArguments)
                {
                    typeAsString += MessagingUtilities.GetParameterTypeAsString(genericType);
                }
                typeAsString += ">";
            }
            if (typeAsString == "Object")
            {
                typeAsString = type.Namespace + "." + typeAsString;
            }
            
            return typeAsString;
        }
        #endregion //Internal
        #endregion //Methods
        #endregion //Class
    }
}