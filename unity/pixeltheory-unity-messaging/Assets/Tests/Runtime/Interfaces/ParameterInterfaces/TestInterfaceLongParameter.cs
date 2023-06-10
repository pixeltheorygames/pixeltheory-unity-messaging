using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceLongParameter
{
    [MessagingTargetAll] public void TestMethodLongSignedParameter(long signedLongParam);
    [MessagingTargetAll] public void TestMethodLongUnsignedParameter(ulong unsignedLongParam);
    [MessagingTargetAll] public void TestMethodLongParameter(long signedLongParam, ulong unsignedLongParam);
}