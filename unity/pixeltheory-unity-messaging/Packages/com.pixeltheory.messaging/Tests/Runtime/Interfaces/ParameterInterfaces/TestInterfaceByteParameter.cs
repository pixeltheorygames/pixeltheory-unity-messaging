using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceByteParameter
{
    void TestMethodByteSignedParameter(sbyte signedByteParam);
    void TestMethodByteUnsignedParameter(byte unsignedByteParam);
    void TestMethodByteParameter(sbyte signedByteParam, byte unsignedByteParam);
}