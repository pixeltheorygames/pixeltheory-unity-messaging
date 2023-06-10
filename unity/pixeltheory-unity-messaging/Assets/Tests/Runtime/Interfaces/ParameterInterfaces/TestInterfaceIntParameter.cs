using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceIntParameter
{
    [MessagingTargetAll] public void TestMethodIntSignedParameter(int signedIntParam);
    [MessagingTargetAll] public void TestMethodIntUnsignedParameter(uint unsignedIntParam);
    [MessagingTargetAll] public void TestMethodIntParameter(int signedIntParam, uint unsignedIntParam);
}