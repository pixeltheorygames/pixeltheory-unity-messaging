using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEditor;
using Pixeltheory.Debug;


namespace Pixeltheory.Messaging.Utilities
{
    public static class MessagingExtensionsGenerator
    {
        #region Class
        #region Methods
        #region Internal
        internal static bool GenerateMessagingExtensions(string eventExtensionsRelativePath, bool stubs)
        {
            string generatorName = nameof(MessagingExtensionsGenerator);
            string eventExtensionsFilenameFull = MessagingConstants.messagingExtensionsFileName + ".cs";
            if (Application.isPlaying)
            {
                string messageFormat = "[{0}] Editor must be in edit mode to generate {1}.";
                Logging.Warn(messageFormat, generatorName, eventExtensionsFilenameFull);
                return false;
            }
            else
            {
                StringBuilder currentTab = new StringBuilder();
                SerializedObject ptMessagingSettings = PixeltheoryMessagingSettings.GetSerializedSettings();
                Type[] messagingInterfaceTypes = MessagingUtilitiesEditor.GetMessagingInterfaceTypes();
                using (StreamWriter outfile = File.CreateText(eventExtensionsRelativePath))
                {
                    outfile.WriteLine("// " + eventExtensionsFilenameFull);
                    outfile.WriteLine("// Auto-Generated " + DateTime.Now);
                    outfile.WriteLine("using System.Collections.Generic;");
                    outfile.WriteLine("using Pixeltheory.Messaging;");
                    List<string> namespaces = new List<string>();
                    namespaces.Add("System.Collections.Generic");
                    namespaces.Add("Pixeltheory.Messaging");
                    foreach (Type messageInterfaceType in messagingInterfaceTypes)
                    {
                        string interfaceNamespace = messageInterfaceType.Namespace;
                        if(!String.IsNullOrEmpty(interfaceNamespace) && !namespaces.Contains(interfaceNamespace))
                        {
                            outfile.WriteLine("using " + interfaceNamespace + ";");
                            namespaces.Add(interfaceNamespace);
                        }
                    }
                    outfile.WriteLine(outfile.NewLine);
                    outfile.WriteLine("namespace " + ptMessagingSettings.FindProperty("messagingExtensionsNamespace").stringValue);
                    outfile.WriteLine("{");
                    currentTab.Append(MessagingConstants.TAB);
                    string classIdentifier = "public static class ";
                    classIdentifier += MessagingConstants.messagingExtensionsFileName;
                    outfile.WriteLine(currentTab + classIdentifier);
                    outfile.Write(currentTab + "{");
                    currentTab.Append(MessagingConstants.TAB);
                    foreach (Type messageInterfaceType in messagingInterfaceTypes)
                    {
                        MethodInfo[] methodInterfaceMethods = messageInterfaceType.GetMethods();
                        if (methodInterfaceMethods.Length > 0)
                        {
                            outfile.Write(outfile.NewLine);
                            outfile.WriteLine(currentTab + "#region " + messageInterfaceType.Name);
                            for (int i = 0; i < methodInterfaceMethods.Length; i++)
                            {
                                MethodInfo methodInfo = methodInterfaceMethods[i];
                                bool isSpreadMessage = methodInfo.GetCustomAttribute<MessagingTargetAll>() != null;
                                bool isSelectMessage = methodInfo.GetCustomAttribute<MessagingTargetSingle>() != null;
                                if (isSpreadMessage || isSelectMessage)
                                {
                                    MessagingExtensionsGenerator.GenerateEventMethod
                                    (
                                        outfile,
                                        currentTab,
                                        messageInterfaceType,
                                        methodInfo,
                                        isSelectMessage,
                                        stubs
                                    );   
                                }
                                if (i < (methodInterfaceMethods.Length - 1))
                                {
                                    outfile.Write(outfile.NewLine);       
                                }
                            }
                            outfile.WriteLine(currentTab + "#endregion");
                        }
                    }
                    currentTab.Remove(currentTab.Length - 1, 1);
                    outfile.WriteLine(currentTab + "}");
                    currentTab.Remove(currentTab.Length - 1, 1);
                    outfile.WriteLine(currentTab + "}");
                    outfile.Flush();
                    outfile.Close();
                }
                AssetDatabase.Refresh();
                return true;
            }
        }
        #endregion //Internal

        #region Private
        private static void GenerateEventMethod(StreamWriter outfile, StringBuilder tab, Type type, MethodInfo method, bool isSelectMessage, bool stub)
        {
            outfile.Write(tab + "public static void " + method.Name + "Event(");
            outfile.Write("this Pixeltheory.Messaging.MessagingManager messagingManager");
            ParameterInfo[] allParameters = method.GetParameters();
            foreach (ParameterInfo parameterInfo in allParameters)
            {
                outfile.Write(", " + MessagingExtensionsGenerator.ProcessParameterInfo(parameterInfo));
            }
            if (isSelectMessage)
            {
                outfile.Write(", System.Int32 targetID");
            }
            outfile.WriteLine(")");
            outfile.WriteLine(tab + "{");
            if (!stub)
            {
                tab.Append(MessagingConstants.TAB);
                outfile.Write(tab + "List<" + type.Name + "> messageListeners = ");
                outfile.Write("messagingManager.GetRegisteredMessageListeners<" + type.Name + ">(\"" + method.DeclaringType.FullName + "." + method.Name + "\"");
                if (isSelectMessage)
                {
                    outfile.Write(", targetID");   
                }
                else
                {
                    outfile.Write(", 0");
                }
                outfile.WriteLine(");");
                outfile.WriteLine(tab + "foreach (" + type.Name +
                                  " listener in messageListeners)");
                outfile.WriteLine( tab + "{");
                tab.Append(MessagingConstants.TAB);
                outfile.Write(tab + "listener." + method.Name + "(");
                bool firstParameter = true;
                foreach (ParameterInfo parameterInfo in allParameters)
                {
                    if (!firstParameter)
                    {
                        outfile.Write(", ");
                    }

                    outfile.Write(parameterInfo.Name);
                    firstParameter = false;
                }

                outfile.WriteLine(");");
                tab.Remove(tab.Length - 1, 1);
                outfile.WriteLine(tab + "}");
            }
            tab.Remove(tab.Length - 1, 1);
            outfile.WriteLine(tab + "}");
        }
        
        private static string ProcessParameterInfo(ParameterInfo parameterInfo)
        {
            string parameter = "";
            Type parameterType = parameterInfo.ParameterType;

            // Out or Ref keywords?
            if (parameterInfo.ParameterType.IsByRef)
            {
                Type referenceType = parameterType.GetElementType();
                if (referenceType != null)
                {
                    
                    parameter += parameterInfo.IsOut ? "out " : "ref ";
                    parameterType = referenceType;
                }
            }

            // Parameter type
            parameter += MessagingExtensionsGenerator.ProcessParameterType(parameterType, parameterType.IsNestedPublic);

            parameter += " ";
            parameter += parameterInfo.Name;
            return parameter;
        }

        private static string ProcessParameterType(Type parameterType, bool nested)
        {
            string parameter = parameterType.ToString();
            
            // Nested
            if (nested)
            {
                parameter = parameter.Replace('+', '.');
            }
            
            // Generic types
            if (parameterType.IsGenericType) //generic parameters
            {
                parameter = parameter.Split('`')[0];
                parameter += "<";
                Type[] allGenericArgumentTypes = parameterType.GetGenericArguments();
                for (int i = 0; i < allGenericArgumentTypes.Length; i++)
                {
                    if (i > 0)
                    {
                        parameter += ", ";
                    }
                    Type genericArgumentType = allGenericArgumentTypes[i];
                    bool isNestedPublic = genericArgumentType.IsNestedPublic;
                    parameter += MessagingExtensionsGenerator.ProcessParameterType(genericArgumentType, isNestedPublic);
                }
                parameter += ">";
            }
            return parameter;
        }
        #endregion //Private
        #endregion //Methods
        #endregion //Class
    }
}