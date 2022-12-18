// MessagingExtensions.cs
// Auto-Generated 7/6/2021 4:56:57 PM
using System.Collections.Generic;
using Pixeltheory;
using Pixeltheory.Messaging;
using TestNamespace;


public static class MessagingExtensions
{
    #region TestInterfaceInheritance
    public static void TestMethodInheritanceEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Quaternion quaternionParam)
    {
        List<TestInterfaceInheritance> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceInheritance>("TestMethodInheritance");
        foreach (TestInterfaceInheritance listener in allListeners)
        {
            listener.TestMethodInheritance(quaternionParam);
        }
    }
    #endregion

    #region TestInterfaceNoNamespace
    public static void TestMethodNoNamespaceNamespacedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestNamespace.TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum)
    {
        List<TestInterfaceNoNamespace> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceNoNamespace>("TestMethodNoNamespaceNamespacedParameter");
        foreach (TestInterfaceNoNamespace listener in allListeners)
        {
            listener.TestMethodNoNamespaceNamespacedParameter(testClassWithNamespaceEnum);
        }
    }

    public static void TestMethodNoNamespaceRegularParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum)
    {
        List<TestInterfaceNoNamespace> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceNoNamespace>("TestMethodNoNamespaceRegularParameter");
        foreach (TestInterfaceNoNamespace listener in allListeners)
        {
            listener.TestMethodNoNamespaceRegularParameter(testClassNoNamespaceEnum);
        }
    }
    #endregion

    #region TestInterfaceBoolParameter
    public static void TestMethodBoolParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Boolean boolParam)
    {
        List<TestInterfaceBoolParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceBoolParameter>("TestMethodBoolParameter");
        foreach (TestInterfaceBoolParameter listener in allListeners)
        {
            listener.TestMethodBoolParameter(boolParam);
        }
    }
    #endregion

    #region TestInterfaceByteParameter
    public static void TestMethodByteSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.SByte signedByteParam)
    {
        List<TestInterfaceByteParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceByteParameter>("TestMethodByteSignedParameter");
        foreach (TestInterfaceByteParameter listener in allListeners)
        {
            listener.TestMethodByteSignedParameter(signedByteParam);
        }
    }

    public static void TestMethodByteUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Byte unsignedByteParam)
    {
        List<TestInterfaceByteParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceByteParameter>("TestMethodByteUnsignedParameter");
        foreach (TestInterfaceByteParameter listener in allListeners)
        {
            listener.TestMethodByteUnsignedParameter(unsignedByteParam);
        }
    }

    public static void TestMethodByteParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.SByte signedByteParam, System.Byte unsignedByteParam)
    {
        List<TestInterfaceByteParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceByteParameter>("TestMethodByteParameter");
        foreach (TestInterfaceByteParameter listener in allListeners)
        {
            listener.TestMethodByteParameter(signedByteParam, unsignedByteParam);
        }
    }
    #endregion

    #region TestInterfaceCharParameter
    public static void TestMethodCharParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Char charParam)
    {
        List<TestInterfaceCharParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceCharParameter>("TestMethodCharParameter");
        foreach (TestInterfaceCharParameter listener in allListeners)
        {
            listener.TestMethodCharParameter(charParam);
        }
    }
    #endregion

    #region TestInterfaceClassParameter
    public static void TestMethodClassParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClass classPrameter)
    {
        List<TestInterfaceClassParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceClassParameter>("TestMethodClassParameter");
        foreach (TestInterfaceClassParameter listener in allListeners)
        {
            listener.TestMethodClassParameter(classPrameter);
        }
    }
    #endregion

    #region TestInterfaceDoubleParameter
    public static void TestMethodDoubleParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Double doubleParam)
    {
        List<TestInterfaceDoubleParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceDoubleParameter>("TestMethodDoubleParameter");
        foreach (TestInterfaceDoubleParameter listener in allListeners)
        {
            listener.TestMethodDoubleParameter(doubleParam);
        }
    }
    #endregion

    #region TestInterfaceEnumParameter
    public static void TestMethodEnumParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestEnum enumParam)
    {
        List<TestInterfaceEnumParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceEnumParameter>("TestMethodEnumParameter");
        foreach (TestInterfaceEnumParameter listener in allListeners)
        {
            listener.TestMethodEnumParameter(enumParam);
        }
    }
    #endregion

    #region TestInterfaceFloatParameter
    public static void TestMethodFloatParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Single floatParam)
    {
        List<TestInterfaceFloatParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceFloatParameter>("TestMethodFloatParameter");
        foreach (TestInterfaceFloatParameter listener in allListeners)
        {
            listener.TestMethodFloatParameter(floatParam);
        }
    }
    #endregion

    #region TestInterfaceGameObjectParameter
    public static void TestMethodGameObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.GameObject gameObjectParameter)
    {
        List<TestInterfaceGameObjectParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceGameObjectParameter>("TestMethodGameObjectParameter");
        foreach (TestInterfaceGameObjectParameter listener in allListeners)
        {
            listener.TestMethodGameObjectParameter(gameObjectParameter);
        }
    }
    #endregion

    #region TestInterfaceIntParameter
    public static void TestMethodIntSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int32 signedIntParam)
    {
        List<TestInterfaceIntParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceIntParameter>("TestMethodIntSignedParameter");
        foreach (TestInterfaceIntParameter listener in allListeners)
        {
            listener.TestMethodIntSignedParameter(signedIntParam);
        }
    }

    public static void TestMethodIntUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.UInt32 unsignedIntParam)
    {
        List<TestInterfaceIntParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceIntParameter>("TestMethodIntUnsignedParameter");
        foreach (TestInterfaceIntParameter listener in allListeners)
        {
            listener.TestMethodIntUnsignedParameter(unsignedIntParam);
        }
    }

    public static void TestMethodIntParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int32 signedIntParam, System.UInt32 unsignedIntParam)
    {
        List<TestInterfaceIntParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceIntParameter>("TestMethodIntParameter");
        foreach (TestInterfaceIntParameter listener in allListeners)
        {
            listener.TestMethodIntParameter(signedIntParam, unsignedIntParam);
        }
    }
    #endregion

    #region TestInterfaceLongParameter
    public static void TestMethodLongSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int64 signedLongParam)
    {
        List<TestInterfaceLongParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceLongParameter>("TestMethodLongSignedParameter");
        foreach (TestInterfaceLongParameter listener in allListeners)
        {
            listener.TestMethodLongSignedParameter(signedLongParam);
        }
    }

    public static void TestMethodLongUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.UInt64 unsignedLongParam)
    {
        List<TestInterfaceLongParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceLongParameter>("TestMethodLongUnsignedParameter");
        foreach (TestInterfaceLongParameter listener in allListeners)
        {
            listener.TestMethodLongUnsignedParameter(unsignedLongParam);
        }
    }

    public static void TestMethodLongParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int64 signedLongParam, System.UInt64 unsignedLongParam)
    {
        List<TestInterfaceLongParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceLongParameter>("TestMethodLongParameter");
        foreach (TestInterfaceLongParameter listener in allListeners)
        {
            listener.TestMethodLongParameter(signedLongParam, unsignedLongParam);
        }
    }
    #endregion

    #region TestInterfaceMonoBehaviourParameter
    public static void TestMethodMonoBehaviourParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.MonoBehaviour monoBehaviourParameter)
    {
        List<TestInterfaceMonoBehaviourParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceMonoBehaviourParameter>("TestMethodMonoBehaviourParameter");
        foreach (TestInterfaceMonoBehaviourParameter listener in allListeners)
        {
            listener.TestMethodMonoBehaviourParameter(monoBehaviourParameter);
        }
    }
    #endregion

    #region TestInterfaceNoParameter
    public static void TestMethodNoParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager)
    {
        List<TestInterfaceNoParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceNoParameter>("TestMethodNoParameter");
        foreach (TestInterfaceNoParameter listener in allListeners)
        {
            listener.TestMethodNoParameter();
        }
    }
    #endregion

    #region TestInterfacePixelBehaviourParameter
    // public static void TestMethodOddBehaviourParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, Pixeltheory.PixelBehaviour<> pixelBehaviourParameter)
    // {
    //     List<TestInterfacePixelBehaviourParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfacePixelBehaviourParameter>("TestMethodOddBehaviourParameter");
    //     foreach (TestInterfacePixelBehaviourParameter listener in allListeners)
    //     {
    //         listener.TestMethodOddBehaviourParameter(pixelBehaviourParameter);
    //     }
    // }
    #endregion

    #region TestInterfacePixelBehaviourSingletonParameter
    public static void TestMethodOddBehaviourSingletonParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassPixelBehaviourSingleton pixelBehaviourSingletonParameter)
    {
        List<TestInterfacePixelBehaviourSingletonParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfacePixelBehaviourSingletonParameter>("TestMethodOddBehaviourSingletonParameter");
        foreach (TestInterfacePixelBehaviourSingletonParameter listener in allListeners)
        {
            listener.TestMethodOddBehaviourSingletonParameter(pixelBehaviourSingletonParameter);
        }
    }
    #endregion

    #region TestInterfacePixelScriptableObjectParameter
    public static void TestMethodOddScriptableObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, Pixeltheory.PixelScriptableObject pixelScriptableObjectParameter)
    {
        List<TestInterfacePixelScriptableObjectParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfacePixelScriptableObjectParameter>("TestMethodOddScriptableObjectParameter");
        foreach (TestInterfacePixelScriptableObjectParameter listener in allListeners)
        {
            listener.TestMethodOddScriptableObjectParameter(pixelScriptableObjectParameter);
        }
    }
    #endregion

    #region TestInterfacePixelScriptableObjectSingletonParameter
    public static void TestMethodPixelScriptableObjectSingletonParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassPixelScriptableObjectSingle pixelScriptableObjectSingletonParameter)
    {
        List<TestInterfacePixelScriptableObjectSingletonParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfacePixelScriptableObjectSingletonParameter>("TestMethodPixelScriptableObjectSingletonParameter");
        foreach (TestInterfacePixelScriptableObjectSingletonParameter listener in allListeners)
        {
            listener.TestMethodPixelScriptableObjectSingletonParameter(pixelScriptableObjectSingletonParameter);
        }
    }
    #endregion

    #region TestInterfaceQuaternionParameter
    public static void TestMethodQuaternionParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Quaternion quaternionParam)
    {
        List<TestInterfaceQuaternionParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceQuaternionParameter>("TestMethodQuaternionParameter");
        foreach (TestInterfaceQuaternionParameter listener in allListeners)
        {
            listener.TestMethodQuaternionParameter(quaternionParam);
        }
    }
    #endregion

    #region TestInterfaceScriptableObjectParameter
    public static void TestMethodScriptableObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.ScriptableObject scriptableObjectParameter)
    {
        List<TestInterfaceScriptableObjectParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceScriptableObjectParameter>("TestMethodScriptableObjectParameter");
        foreach (TestInterfaceScriptableObjectParameter listener in allListeners)
        {
            listener.TestMethodScriptableObjectParameter(scriptableObjectParameter);
        }
    }
    #endregion

    #region TestInterfaceShortParameter
    public static void TestMethodShortSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int16 signedShortParam)
    {
        List<TestInterfaceShortParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceShortParameter>("TestMethodShortSignedParameter");
        foreach (TestInterfaceShortParameter listener in allListeners)
        {
            listener.TestMethodShortSignedParameter(signedShortParam);
        }
    }

    public static void TestMethodShortUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.UInt16 unsignedShortParam)
    {
        List<TestInterfaceShortParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceShortParameter>("TestMethodShortUnsignedParameter");
        foreach (TestInterfaceShortParameter listener in allListeners)
        {
            listener.TestMethodShortUnsignedParameter(unsignedShortParam);
        }
    }

    public static void TestMethodShortParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int16 signedShortParam, System.UInt16 unsignedShortParam)
    {
        List<TestInterfaceShortParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceShortParameter>("TestMethodShortParameter");
        foreach (TestInterfaceShortParameter listener in allListeners)
        {
            listener.TestMethodShortParameter(signedShortParam, unsignedShortParam);
        }
    }
    #endregion

    #region TestInterfaceStringParameter
    public static void TestMethodStringParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.String stringParam)
    {
        List<TestInterfaceStringParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceStringParameter>("TestMethodStringParameter");
        foreach (TestInterfaceStringParameter listener in allListeners)
        {
            listener.TestMethodStringParameter(stringParam);
        }
    }
    #endregion

    #region TestInterfaceStructParameter
    public static void TestMethodStructParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestStruct structParam)
    {
        List<TestInterfaceStructParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceStructParameter>("TestMethodStructParameter");
        foreach (TestInterfaceStructParameter listener in allListeners)
        {
            listener.TestMethodStructParameter(structParam);
        }
    }
    #endregion

    #region TestInterfaceUnityEngineObjectParameter
    public static void TestMethodUnityEngineObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Object unityEngineObjectParameter)
    {
        List<TestInterfaceUnityEngineObjectParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceUnityEngineObjectParameter>("TestMethodUnityEngineObjectParameter");
        foreach (TestInterfaceUnityEngineObjectParameter listener in allListeners)
        {
            listener.TestMethodUnityEngineObjectParameter(unityEngineObjectParameter);
        }
    }
    #endregion

    #region TestInterfaceVectorParameter
    public static void TestMethodVector2ParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector2 vector2Param)
    {
        List<TestInterfaceVectorParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceVectorParameter>("TestMethodVector2Parameter");
        foreach (TestInterfaceVectorParameter listener in allListeners)
        {
            listener.TestMethodVector2Parameter(vector2Param);
        }
    }

    public static void TestMethodVector3ParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector3 vector3Param)
    {
        List<TestInterfaceVectorParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceVectorParameter>("TestMethodVector3Parameter");
        foreach (TestInterfaceVectorParameter listener in allListeners)
        {
            listener.TestMethodVector3Parameter(vector3Param);
        }
    }

    public static void TestMethodVector4ParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector4 vector4Param)
    {
        List<TestInterfaceVectorParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceVectorParameter>("TestMethodVector4Parameter");
        foreach (TestInterfaceVectorParameter listener in allListeners)
        {
            listener.TestMethodVector4Parameter(vector4Param);
        }
    }

    public static void TestMethodVector2IntParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector2Int vector2IntParam)
    {
        List<TestInterfaceVectorParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceVectorParameter>("TestMethodVector2IntParameter");
        foreach (TestInterfaceVectorParameter listener in allListeners)
        {
            listener.TestMethodVector2IntParameter(vector2IntParam);
        }
    }

    public static void TestMethodVector3IntParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector3Int vector3IntParam)
    {
        List<TestInterfaceVectorParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceVectorParameter>("TestMethodVector3IntParameter");
        foreach (TestInterfaceVectorParameter listener in allListeners)
        {
            listener.TestMethodVector3IntParameter(vector3IntParam);
        }
    }
    #endregion

    #region TestInterfaceArrayParameter
    public static void TestMethodIntArrayParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int32[] arrayParam)
    {
        List<TestInterfaceArrayParameter> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceArrayParameter>("TestMethodIntArrayParameter");
        foreach (TestInterfaceArrayParameter listener in allListeners)
        {
            listener.TestMethodIntArrayParameter(arrayParam);
        }
    }
    #endregion

    #region TestInterfaceInNamespace
    public static void TestMethodInNamespaceNamespacedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestNamespace.TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum)
    {
        List<TestInterfaceInNamespace> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceInNamespace>("TestMethodInNamespaceNamespacedParameter");
        foreach (TestInterfaceInNamespace listener in allListeners)
        {
            listener.TestMethodInNamespaceNamespacedParameter(testClassWithNamespaceEnum);
        }
    }

    public static void TestMethodInNamespaceRegularParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum)
    {
        List<TestInterfaceInNamespace> allListeners = messagingManager.GetRegisteredListeners<TestInterfaceInNamespace>("TestMethodInNamespaceRegularParameter");
        foreach (TestInterfaceInNamespace listener in allListeners)
        {
            listener.TestMethodInNamespaceRegularParameter(testClassNoNamespaceEnum);
        }
    }
    #endregion
}
