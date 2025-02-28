using System;
using NUnit.Framework.Internal;
using Pixeltheory;
using UnityEngine;
using Pixeltheory.Debug;
using Pixeltheory.Messaging;
using TestNamespace;

using Random = UnityEngine.Random;


public class TestMessagingReceiver : MessagingBehaviour, 
    TestInterfaceInheritance, TestInterfaceNoNamespace, TestInterfaceInNamespace, TestInterfaceNoParameter
{
    public void TestMethodByteSignedParameter(sbyte signedByteParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodByteSignedParameter",
            signedByteParam.ToString()
        );
    }

    public void TestMethodByteUnsignedParameter(byte unsignedByteParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodByteUnsignedParameter",
            unsignedByteParam.ToString()
        );
    }

    public void TestMethodByteParameter(sbyte signedByteParam, byte unsignedByteParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with arguments {2} {3}",
            this.GetType().Name,
            "TestMethodByteParameter",
            signedByteParam.ToString(),
            unsignedByteParam.ToString()
        );
    }

    public void TestMethodShortSignedParameter(short signedShortParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodShortSignedParameter",
            signedShortParam.ToString()
        );
    }

    public void TestMethodShortUnsignedParameter(ushort unsignedShortParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodShortUnsignedParameter",
            unsignedShortParam.ToString()
        );
    }

    public void TestMethodShortParameter(short signedShortParam, ushort unsignedShortParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with arguments {2} {3}",
            this.GetType().Name,
            "TestMethodShortParameter",
            signedShortParam.ToString(),
            unsignedShortParam.ToString()
        );
    }

    public void TestMethodIntSignedParameter(int signedIntParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodIntSignedParameter",
            signedIntParam.ToString()
        );
    }

    public void TestMethodIntUnsignedParameter(uint unsignedIntParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodIntUnsignedParameter",
            unsignedIntParam.ToString()
        );
    }

    public void TestMethodIntParameter(int signedIntParam, uint unsignedIntParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with arguments {2} {3}",
            this.GetType().Name,
            "TestMethodIntParameter",
            signedIntParam.ToString(),
            unsignedIntParam.ToString()
        );
    }

    public void TestMethodLongSignedParameter(long signedLongParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodLongSignedParameter",
            signedLongParam.ToString()
        );
    }

    public void TestMethodLongUnsignedParameter(ulong unsignedLongParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodLongUnsignedParameter",
            unsignedLongParam.ToString()
        );
    }

    public void TestMethodLongParameter(long signedLongParam, ulong unsignedLongParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with arguments {2} {3}",
            this.GetType().Name,
            "TestMethodLongParameter",
            signedLongParam.ToString(),
            unsignedLongParam.ToString()
        );
    }

    public void TestMethodCharParameter(char charParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodCharParameter",
            charParam.ToString()
        );
    }

    public void TestMethodFloatParameter(float floatParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodFloatParameter",
            floatParam.ToString()
        );
    }

    public void TestMethodDoubleParameter(double doubleParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodDoubleParameter",
            doubleParam.ToString()
        );
    }

    public void TestMethodBoolParameter(bool boolParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodBoolParameter",
            boolParam.ToString()
        );
    }

    public void TestMethodEnumParameter(TestEnum enumParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodEnumParameter",
            enumParam.ToString()
        );
    }

    public void TestMethodStructParameter(TestStruct structParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodStructParameter",
            structParam.ToString()
        );
    }

    public void TestMethodStringParameter(string stringParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodStringParameter",
            stringParam
        );
    }

    public void TestMethodClassParameter(TestClass classPrameter)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodClassParameter",
            classPrameter.ToString()
        );
    }

    public void TestMethodIntArrayParameter(int[] arrayParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodArrayParameter",
            arrayParam.ToString()
        );
    }

    public void TestMethodInheritance(Quaternion quaternionParam)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodInheritance",
            quaternionParam.ToString()
        );
    }

    public void TestMethodNoNamespaceNamespacedParameter(TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodNoNamespaceNamespacedParameter",
            testClassWithNamespaceEnum.ToString()
        );
    }

    public void TestMethodNoNamespaceRegularParameter(TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodNoNamespaceRegularParameter",
            testClassNoNamespaceEnum.ToString()
        );
    }

    public void TestMethodInNamespaceNamespacedParameter(TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodInNamespaceNamespacedParameter",
            testClassWithNamespaceEnum.ToString()
        );
    }

    public void TestMethodInNamespaceRegularParameter(TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodInNamespaceRegularParameter",
            testClassNoNamespaceEnum.ToString()
        );
    }

    public void TestMethodGameObjectParameter(GameObject gameObjectParameter)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodGameObjectParameter",
            gameObjectParameter.ToString()
        );
    }
    
    public void TestMethodMonoBehaviourParameter(MonoBehaviour monoBehaviourParameter)
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with argument {2}",
            this.GetType().Name,
            "TestMethodMonoBehaviourParameter",
            monoBehaviourParameter.ToString()
        );
    }

    public void TestMethodNoParameter()
    {
        PixelLog.Log
        (
            "[{0}] {1} received message with no arguments",
            this.GetType().Name,
            "TestMethodNoParameter"
        );
    }
}
