using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Pixeltheory.Debug;


namespace Pixeltheory.Messaging.Editor
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
                Type[] messagingInterfaceTypes = MessagingUtilities.GetMessagingInterfaceTypes();
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
                    string classIdentifier = "public static class ";
                    classIdentifier += MessagingConstants.messagingExtensionsFileName;
                    outfile.WriteLine(classIdentifier);
                    outfile.Write("{");
                    foreach (Type messageInterfaceType in messagingInterfaceTypes)
                    {
                        MethodInfo[] methodInterfaceMethods = messageInterfaceType.GetMethods();
                        if (methodInterfaceMethods.Length > 0)
                        {
                            outfile.Write(outfile.NewLine);
                            outfile.WriteLine("    #region " + messageInterfaceType.Name);
                            MessagingExtensionsGenerator.GenerateEventMethod
                            (
                                outfile,
                                messageInterfaceType,
                                methodInterfaceMethods[0],
                                stubs
                            );
                            for (int i = 1; i < methodInterfaceMethods.Length; i++)
                            {
                                outfile.Write(outfile.NewLine);
                                MessagingExtensionsGenerator.GenerateEventMethod
                                (
                                    outfile,
                                    messageInterfaceType,
                                    methodInterfaceMethods[i],
                                    stubs
                                );
                            }
                            outfile.WriteLine("    #endregion");
                        }
                    }
                    outfile.WriteLine("}");
                    outfile.Flush();
                    outfile.Close();
                }
                AssetDatabase.Refresh();
                return true;
            }
        }
        #endregion //Internal

        #region Private
        private static void GenerateEventMethod(StreamWriter outfile, Type type, MethodInfo method, bool stub)
        {
            outfile.Write("    public static void " + method.Name + "Event(");
            outfile.Write("this Pixeltheory.Messaging.MessagingManager messagingManager");
            ParameterInfo[] allParameters = method.GetParameters();
            foreach (ParameterInfo parameterInfo in allParameters)
            {
                outfile.Write(", " + MessagingExtensionsGenerator.ProcessParameterInfo(parameterInfo));
            }
            outfile.WriteLine(")");
            outfile.WriteLine("    {");
            if (!stub)
            {
                outfile.Write("        List<" + type.Name + "> allListeners = ");
                outfile.WriteLine("messagingManager.GetRegisteredListeners<" +
                                  type.Name + ">(\"" + method.Name +
                                  "\");");
                outfile.WriteLine("        foreach (" + type.Name +
                                  " listener in allListeners)");
                outfile.WriteLine("        {");
                outfile.Write("            listener." + method.Name + "(");
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
                outfile.WriteLine("        }");
            }
            outfile.WriteLine("    }");
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