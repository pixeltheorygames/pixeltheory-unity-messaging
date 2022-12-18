using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceShortParameter
{
    void TestMethodShortSignedParameter(short signedShortParam);
    void TestMethodShortUnsignedParameter(ushort unsignedShortParam);
    void TestMethodShortParameter(short signedShortParam, ushort unsignedShortParam);
}