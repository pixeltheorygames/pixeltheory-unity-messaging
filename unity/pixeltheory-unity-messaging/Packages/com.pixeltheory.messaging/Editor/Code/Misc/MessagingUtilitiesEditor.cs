using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using Pixeltheory.Debug;
using UnityEditor.PackageManager.UI;
using Assembly = System.Reflection.Assembly;
using UnityAssembly = UnityEditor.Compilation.Assembly;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;


namespace Pixeltheory.Messaging.Utilities
{
    public class MessagingUtilitiesEditor
    {
        #region Class
        #region Methods
        #region Internal
        internal static string GetSaveAssetPath(string filename, string fileExtension)
        {
            string filenameFull = filename + fileExtension;
            string saveFileSelectedPath =
                EditorUtility.SaveFolderPanel
                (
                    "Folder to save/overwrite " + filenameFull + " to",
                    "",
                    filenameFull
                );
            if (saveFileSelectedPath == string.Empty)
            {
                // User cancelled out of selecting a location
                PixelLog.Warn("[MessagingManager] No location selected for {0}; exiting.", filenameFull);
                return null;
            }
            if (File.Exists(saveFileSelectedPath + Path.DirectorySeparatorChar + filenameFull))
            {
                PixelLog.Log("[MessagingManager] Found existing file at selected location for {0}; overwriting.", filenameFull);
            }
            else
            {
                PixelLog.Log("[MessagingManager] No existing file found at selected location for {0}; creating.", filenameFull);
            }
            return saveFileSelectedPath + Path.DirectorySeparatorChar + filenameFull;
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
            UnityAssembly[] playerAssemblies = CompilationPipeline.GetAssemblies(AssembliesType.PlayerWithoutTestAssemblies);
            Dictionary<string, UnityAssembly> unityAssembliesMap = new Dictionary<string, UnityAssembly>();
            List<Assembly> playerFilteredAssemblies = new List<Assembly>();
            List<Assembly> finalAssemblyList = new List<Assembly>();
            List<Type> interfaceTypes = new List<Type>();
            foreach (UnityAssembly unityAssembly in playerAssemblies)
            {
                unityAssembliesMap.Add(unityAssembly.name, unityAssembly);
            }
            foreach (Assembly assembly in allAssemblies)
            {
                if (unityAssembliesMap.TryGetValue(assembly.GetName().Name, out UnityAssembly unityAssembly))
                {
                    playerFilteredAssemblies.Add(assembly);
                }
            }
            foreach (Assembly playerAssembly in playerFilteredAssemblies)
            {
                PackageInfo packageInfo = PackageInfo.FindForAssembly(playerAssembly);
                if (packageInfo == null || packageInfo.source == PackageSource.Embedded || packageInfo.source == PackageSource.Local)
                {
                    finalAssemblyList.Add(playerAssembly);
                }
            }
            foreach (Assembly assembly in finalAssemblyList)
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
                    typeAsString += MessagingUtilitiesEditor.GetParameterTypeAsString(genericType);
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