using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceShortParameter
{
    [MessagingTargetAll] public void TestMethodShortSignedParameter(short signedShortParam);
    [MessagingTargetAll] public void TestMethodShortUnsignedParameter(ushort unsignedShortParam);
    [MessagingTargetAll] public void TestMethodShortParameter(short signedShortParam, ushort unsignedShortParam);
}