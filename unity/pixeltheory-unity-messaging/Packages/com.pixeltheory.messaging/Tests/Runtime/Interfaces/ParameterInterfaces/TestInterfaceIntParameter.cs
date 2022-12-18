using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceIntParameter
{
    void TestMethodIntSignedParameter(int signedIntParam);
    void TestMethodIntUnsignedParameter(uint unsignedIntParam);
    void TestMethodIntParameter(int signedIntParam, uint unsignedIntParam);
}