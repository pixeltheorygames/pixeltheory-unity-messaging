using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceByteParameter
{
    [MessagingTargetAll] public void TestMethodByteSignedParameter(sbyte signedByteParam);
    [MessagingTargetAll] public void TestMethodByteUnsignedParameter(byte unsignedByteParam);
    [MessagingTargetAll] public void TestMethodByteParameter(sbyte signedByteParam, byte unsignedByteParam);
}