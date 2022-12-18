using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceLongParameter
{
    void TestMethodLongSignedParameter(long signedLongParam);
    void TestMethodLongUnsignedParameter(ulong unsignedLongParam);
    void TestMethodLongParameter(long signedLongParam, ulong unsignedLongParam);
}