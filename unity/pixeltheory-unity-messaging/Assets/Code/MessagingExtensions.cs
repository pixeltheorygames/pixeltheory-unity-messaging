// MessagingExtensions.cs
// Auto-Generated 2/27/2023 3:26:35 AM
using System.Collections.Generic;
using Pixeltheory.Messaging;
using Pixeltheory.Messaging.Tests;
using TestNamespace;


namespace Pixeltheory.Messaging.Tests
{
	public static class MessagingExtensions
	{
		#region ITestMessagingTargetAll
		public static void TestMessagingTargetAllIntEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup, System.Int32 testInt)
		{
			List<ITestMessagingTargetAll> messageListeners = messagingManager.GetRegisteredMessageListeners<ITestMessagingTargetAll>("Pixeltheory.Messaging.Tests.ITestMessagingTargetAll.TestMessagingTargetAllInt", 0);
			foreach (ITestMessagingTargetAll listener in messageListeners)
			{
				listener.TestMessagingTargetAllInt(realtimeSinceStartup, testInt);
			}
		}
		#endregion

		#region ITestMessagingTargetSingle
		public static void TestMessagingTargetSingleIntEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Single realtimeSinceStartup, System.Int32 testInt, System.Int32 targetID)
		{
			List<ITestMessagingTargetSingle> messageListeners = messagingManager.GetRegisteredMessageListeners<ITestMessagingTargetSingle>("Pixeltheory.Messaging.Tests.ITestMessagingTargetSingle.TestMessagingTargetSingleInt", targetID);
			foreach (ITestMessagingTargetSingle listener in messageListeners)
			{
				listener.TestMessagingTargetSingleInt(realtimeSinceStartup, testInt);
			}
		}
		#endregion

		#region TestInterfaceInheritance
		public static void TestMethodInheritanceEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Quaternion quaternionParam)
		{
			List<TestInterfaceInheritance> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceInheritance>("TestInterfaceInheritance.TestMethodInheritance", 0);
			foreach (TestInterfaceInheritance listener in messageListeners)
			{
				listener.TestMethodInheritance(quaternionParam);
			}
		}
		#endregion

		#region TestInterfaceNoNamespace
		public static void TestMethodNoNamespaceNamespacedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestNamespace.TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum)
		{
			List<TestInterfaceNoNamespace> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceNoNamespace>("TestInterfaceNoNamespace.TestMethodNoNamespaceNamespacedParameter", 0);
			foreach (TestInterfaceNoNamespace listener in messageListeners)
			{
				listener.TestMethodNoNamespaceNamespacedParameter(testClassWithNamespaceEnum);
			}
		}

		public static void TestMethodNoNamespaceRegularParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum)
		{
			List<TestInterfaceNoNamespace> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceNoNamespace>("TestInterfaceNoNamespace.TestMethodNoNamespaceRegularParameter", 0);
			foreach (TestInterfaceNoNamespace listener in messageListeners)
			{
				listener.TestMethodNoNamespaceRegularParameter(testClassNoNamespaceEnum);
			}
		}
		#endregion

		#region TestInterfaceBoolParameter
		public static void TestMethodBoolParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Boolean boolParam)
		{
			List<TestInterfaceBoolParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceBoolParameter>("TestInterfaceBoolParameter.TestMethodBoolParameter", 0);
			foreach (TestInterfaceBoolParameter listener in messageListeners)
			{
				listener.TestMethodBoolParameter(boolParam);
			}
		}
		#endregion

		#region TestInterfaceByteParameter
		public static void TestMethodByteSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.SByte signedByteParam)
		{
			List<TestInterfaceByteParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceByteParameter>("TestInterfaceByteParameter.TestMethodByteSignedParameter", 0);
			foreach (TestInterfaceByteParameter listener in messageListeners)
			{
				listener.TestMethodByteSignedParameter(signedByteParam);
			}
		}

		public static void TestMethodByteUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Byte unsignedByteParam)
		{
			List<TestInterfaceByteParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceByteParameter>("TestInterfaceByteParameter.TestMethodByteUnsignedParameter", 0);
			foreach (TestInterfaceByteParameter listener in messageListeners)
			{
				listener.TestMethodByteUnsignedParameter(unsignedByteParam);
			}
		}

		public static void TestMethodByteParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.SByte signedByteParam, System.Byte unsignedByteParam)
		{
			List<TestInterfaceByteParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceByteParameter>("TestInterfaceByteParameter.TestMethodByteParameter", 0);
			foreach (TestInterfaceByteParameter listener in messageListeners)
			{
				listener.TestMethodByteParameter(signedByteParam, unsignedByteParam);
			}
		}
		#endregion

		#region TestInterfaceCharParameter
		public static void TestMethodCharParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Char charParam)
		{
			List<TestInterfaceCharParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceCharParameter>("TestInterfaceCharParameter.TestMethodCharParameter", 0);
			foreach (TestInterfaceCharParameter listener in messageListeners)
			{
				listener.TestMethodCharParameter(charParam);
			}
		}
		#endregion

		#region TestInterfaceClassParameter
		public static void TestMethodClassParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClass classPrameter)
		{
			List<TestInterfaceClassParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceClassParameter>("TestInterfaceClassParameter.TestMethodClassParameter", 0);
			foreach (TestInterfaceClassParameter listener in messageListeners)
			{
				listener.TestMethodClassParameter(classPrameter);
			}
		}
		#endregion

		#region TestInterfaceDoubleParameter
		public static void TestMethodDoubleParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Double doubleParam)
		{
			List<TestInterfaceDoubleParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceDoubleParameter>("TestInterfaceDoubleParameter.TestMethodDoubleParameter", 0);
			foreach (TestInterfaceDoubleParameter listener in messageListeners)
			{
				listener.TestMethodDoubleParameter(doubleParam);
			}
		}
		#endregion

		#region TestInterfaceEnumParameter
		public static void TestMethodEnumParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestEnum enumParam)
		{
			List<TestInterfaceEnumParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceEnumParameter>("TestInterfaceEnumParameter.TestMethodEnumParameter", 0);
			foreach (TestInterfaceEnumParameter listener in messageListeners)
			{
				listener.TestMethodEnumParameter(enumParam);
			}
		}
		#endregion

		#region TestInterfaceFloatParameter
		public static void TestMethodFloatParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Single floatParam)
		{
			List<TestInterfaceFloatParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceFloatParameter>("TestInterfaceFloatParameter.TestMethodFloatParameter", 0);
			foreach (TestInterfaceFloatParameter listener in messageListeners)
			{
				listener.TestMethodFloatParameter(floatParam);
			}
		}
		#endregion

		#region TestInterfaceGameObjectParameter
		public static void TestMethodGameObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.GameObject gameObjectParameter)
		{
			List<TestInterfaceGameObjectParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceGameObjectParameter>("TestInterfaceGameObjectParameter.TestMethodGameObjectParameter", 0);
			foreach (TestInterfaceGameObjectParameter listener in messageListeners)
			{
				listener.TestMethodGameObjectParameter(gameObjectParameter);
			}
		}
		#endregion

		#region TestInterfaceIntParameter
		public static void TestMethodIntSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int32 signedIntParam)
		{
			List<TestInterfaceIntParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceIntParameter>("TestInterfaceIntParameter.TestMethodIntSignedParameter", 0);
			foreach (TestInterfaceIntParameter listener in messageListeners)
			{
				listener.TestMethodIntSignedParameter(signedIntParam);
			}
		}

		public static void TestMethodIntUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.UInt32 unsignedIntParam)
		{
			List<TestInterfaceIntParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceIntParameter>("TestInterfaceIntParameter.TestMethodIntUnsignedParameter", 0);
			foreach (TestInterfaceIntParameter listener in messageListeners)
			{
				listener.TestMethodIntUnsignedParameter(unsignedIntParam);
			}
		}

		public static void TestMethodIntParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int32 signedIntParam, System.UInt32 unsignedIntParam)
		{
			List<TestInterfaceIntParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceIntParameter>("TestInterfaceIntParameter.TestMethodIntParameter", 0);
			foreach (TestInterfaceIntParameter listener in messageListeners)
			{
				listener.TestMethodIntParameter(signedIntParam, unsignedIntParam);
			}
		}
		#endregion

		#region TestInterfaceLongParameter
		public static void TestMethodLongSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int64 signedLongParam)
		{
			List<TestInterfaceLongParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceLongParameter>("TestInterfaceLongParameter.TestMethodLongSignedParameter", 0);
			foreach (TestInterfaceLongParameter listener in messageListeners)
			{
				listener.TestMethodLongSignedParameter(signedLongParam);
			}
		}

		public static void TestMethodLongUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.UInt64 unsignedLongParam)
		{
			List<TestInterfaceLongParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceLongParameter>("TestInterfaceLongParameter.TestMethodLongUnsignedParameter", 0);
			foreach (TestInterfaceLongParameter listener in messageListeners)
			{
				listener.TestMethodLongUnsignedParameter(unsignedLongParam);
			}
		}

		public static void TestMethodLongParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int64 signedLongParam, System.UInt64 unsignedLongParam)
		{
			List<TestInterfaceLongParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceLongParameter>("TestInterfaceLongParameter.TestMethodLongParameter", 0);
			foreach (TestInterfaceLongParameter listener in messageListeners)
			{
				listener.TestMethodLongParameter(signedLongParam, unsignedLongParam);
			}
		}
		#endregion

		#region TestInterfaceMonoBehaviourParameter
		public static void TestMethodMonoBehaviourParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.MonoBehaviour monoBehaviourParameter)
		{
			List<TestInterfaceMonoBehaviourParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceMonoBehaviourParameter>("TestInterfaceMonoBehaviourParameter.TestMethodMonoBehaviourParameter", 0);
			foreach (TestInterfaceMonoBehaviourParameter listener in messageListeners)
			{
				listener.TestMethodMonoBehaviourParameter(monoBehaviourParameter);
			}
		}
		#endregion

		#region TestInterfaceNoParameter
		public static void TestMethodNoParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager)
		{
			List<TestInterfaceNoParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceNoParameter>("TestInterfaceNoParameter.TestMethodNoParameter", 0);
			foreach (TestInterfaceNoParameter listener in messageListeners)
			{
				listener.TestMethodNoParameter();
			}
		}
		#endregion

		#region TestInterfacePixelBehaviourSingletonParameter
		public static void TestMethodOddBehaviourSingletonParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassPixelBehaviourSingleton pixelBehaviourSingletonParameter)
		{
			List<TestInterfacePixelBehaviourSingletonParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfacePixelBehaviourSingletonParameter>("TestInterfacePixelBehaviourSingletonParameter.TestMethodOddBehaviourSingletonParameter", 0);
			foreach (TestInterfacePixelBehaviourSingletonParameter listener in messageListeners)
			{
				listener.TestMethodOddBehaviourSingletonParameter(pixelBehaviourSingletonParameter);
			}
		}
		#endregion

		#region TestInterfacePixelScriptableObjectParameter
		public static void TestMethodOddScriptableObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, Pixeltheory.PixelObject pixelScriptableObjectParameter)
		{
			List<TestInterfacePixelScriptableObjectParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfacePixelScriptableObjectParameter>("TestInterfacePixelScriptableObjectParameter.TestMethodOddScriptableObjectParameter", 0);
			foreach (TestInterfacePixelScriptableObjectParameter listener in messageListeners)
			{
				listener.TestMethodOddScriptableObjectParameter(pixelScriptableObjectParameter);
			}
		}
		#endregion

		#region TestInterfacePixelScriptableObjectSingletonParameter
		public static void TestMethodPixelScriptableObjectSingletonParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassPixelScriptableObjectSingle pixelScriptableObjectSingletonParameter)
		{
			List<TestInterfacePixelScriptableObjectSingletonParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfacePixelScriptableObjectSingletonParameter>("TestInterfacePixelScriptableObjectSingletonParameter.TestMethodPixelScriptableObjectSingletonParameter", 0);
			foreach (TestInterfacePixelScriptableObjectSingletonParameter listener in messageListeners)
			{
				listener.TestMethodPixelScriptableObjectSingletonParameter(pixelScriptableObjectSingletonParameter);
			}
		}
		#endregion

		#region TestInterfaceQuaternionParameter
		public static void TestMethodQuaternionParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Quaternion quaternionParam)
		{
			List<TestInterfaceQuaternionParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceQuaternionParameter>("TestInterfaceQuaternionParameter.TestMethodQuaternionParameter", 0);
			foreach (TestInterfaceQuaternionParameter listener in messageListeners)
			{
				listener.TestMethodQuaternionParameter(quaternionParam);
			}
		}
		#endregion

		#region TestInterfaceScriptableObjectParameter
		public static void TestMethodScriptableObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.ScriptableObject scriptableObjectParameter)
		{
			List<TestInterfaceScriptableObjectParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceScriptableObjectParameter>("TestInterfaceScriptableObjectParameter.TestMethodScriptableObjectParameter", 0);
			foreach (TestInterfaceScriptableObjectParameter listener in messageListeners)
			{
				listener.TestMethodScriptableObjectParameter(scriptableObjectParameter);
			}
		}
		#endregion

		#region TestInterfaceShortParameter
		public static void TestMethodShortSignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int16 signedShortParam)
		{
			List<TestInterfaceShortParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceShortParameter>("TestInterfaceShortParameter.TestMethodShortSignedParameter", 0);
			foreach (TestInterfaceShortParameter listener in messageListeners)
			{
				listener.TestMethodShortSignedParameter(signedShortParam);
			}
		}

		public static void TestMethodShortUnsignedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.UInt16 unsignedShortParam)
		{
			List<TestInterfaceShortParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceShortParameter>("TestInterfaceShortParameter.TestMethodShortUnsignedParameter", 0);
			foreach (TestInterfaceShortParameter listener in messageListeners)
			{
				listener.TestMethodShortUnsignedParameter(unsignedShortParam);
			}
		}

		public static void TestMethodShortParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int16 signedShortParam, System.UInt16 unsignedShortParam)
		{
			List<TestInterfaceShortParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceShortParameter>("TestInterfaceShortParameter.TestMethodShortParameter", 0);
			foreach (TestInterfaceShortParameter listener in messageListeners)
			{
				listener.TestMethodShortParameter(signedShortParam, unsignedShortParam);
			}
		}
		#endregion

		#region TestInterfaceStringParameter
		public static void TestMethodStringParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.String stringParam)
		{
			List<TestInterfaceStringParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceStringParameter>("TestInterfaceStringParameter.TestMethodStringParameter", 0);
			foreach (TestInterfaceStringParameter listener in messageListeners)
			{
				listener.TestMethodStringParameter(stringParam);
			}
		}
		#endregion

		#region TestInterfaceStructParameter
		public static void TestMethodStructParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestStruct structParam)
		{
			List<TestInterfaceStructParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceStructParameter>("TestInterfaceStructParameter.TestMethodStructParameter", 0);
			foreach (TestInterfaceStructParameter listener in messageListeners)
			{
				listener.TestMethodStructParameter(structParam);
			}
		}
		#endregion

		#region TestInterfaceUnityEngineObjectParameter
		public static void TestMethodUnityEngineObjectParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Object unityEngineObjectParameter)
		{
			List<TestInterfaceUnityEngineObjectParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceUnityEngineObjectParameter>("TestInterfaceUnityEngineObjectParameter.TestMethodUnityEngineObjectParameter", 0);
			foreach (TestInterfaceUnityEngineObjectParameter listener in messageListeners)
			{
				listener.TestMethodUnityEngineObjectParameter(unityEngineObjectParameter);
			}
		}
		#endregion

		#region TestInterfaceVectorParameter
		public static void TestMethodVector2ParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector2 vector2Param)
		{
			List<TestInterfaceVectorParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceVectorParameter>("TestInterfaceVectorParameter.TestMethodVector2Parameter", 0);
			foreach (TestInterfaceVectorParameter listener in messageListeners)
			{
				listener.TestMethodVector2Parameter(vector2Param);
			}
		}

		public static void TestMethodVector3ParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector3 vector3Param)
		{
			List<TestInterfaceVectorParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceVectorParameter>("TestInterfaceVectorParameter.TestMethodVector3Parameter", 0);
			foreach (TestInterfaceVectorParameter listener in messageListeners)
			{
				listener.TestMethodVector3Parameter(vector3Param);
			}
		}

		public static void TestMethodVector4ParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector4 vector4Param)
		{
			List<TestInterfaceVectorParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceVectorParameter>("TestInterfaceVectorParameter.TestMethodVector4Parameter", 0);
			foreach (TestInterfaceVectorParameter listener in messageListeners)
			{
				listener.TestMethodVector4Parameter(vector4Param);
			}
		}

		public static void TestMethodVector2IntParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector2Int vector2IntParam)
		{
			List<TestInterfaceVectorParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceVectorParameter>("TestInterfaceVectorParameter.TestMethodVector2IntParameter", 0);
			foreach (TestInterfaceVectorParameter listener in messageListeners)
			{
				listener.TestMethodVector2IntParameter(vector2IntParam);
			}
		}

		public static void TestMethodVector3IntParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, UnityEngine.Vector3Int vector3IntParam)
		{
			List<TestInterfaceVectorParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceVectorParameter>("TestInterfaceVectorParameter.TestMethodVector3IntParameter", 0);
			foreach (TestInterfaceVectorParameter listener in messageListeners)
			{
				listener.TestMethodVector3IntParameter(vector3IntParam);
			}
		}
		#endregion

		#region TestInterfaceArrayParameter
		public static void TestMethodIntArrayParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, System.Int32[] arrayParam)
		{
			List<TestInterfaceArrayParameter> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceArrayParameter>("TestInterfaceArrayParameter.TestMethodIntArrayParameter", 0);
			foreach (TestInterfaceArrayParameter listener in messageListeners)
			{
				listener.TestMethodIntArrayParameter(arrayParam);
			}
		}
		#endregion

		#region TestInterfaceInNamespace
		public static void TestMethodInNamespaceNamespacedParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestNamespace.TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum)
		{
			List<TestInterfaceInNamespace> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceInNamespace>("TestNamespace.TestInterfaceInNamespace.TestMethodInNamespaceNamespacedParameter", 0);
			foreach (TestInterfaceInNamespace listener in messageListeners)
			{
				listener.TestMethodInNamespaceNamespacedParameter(testClassWithNamespaceEnum);
			}
		}

		public static void TestMethodInNamespaceRegularParameterEvent(this Pixeltheory.Messaging.MessagingManager messagingManager, TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum)
		{
			List<TestInterfaceInNamespace> messageListeners = messagingManager.GetRegisteredMessageListeners<TestInterfaceInNamespace>("TestNamespace.TestInterfaceInNamespace.TestMethodInNamespaceRegularParameter", 0);
			foreach (TestInterfaceInNamespace listener in messageListeners)
			{
				listener.TestMethodInNamespaceRegularParameter(testClassNoNamespaceEnum);
			}
		}
		#endregion
	}
}
