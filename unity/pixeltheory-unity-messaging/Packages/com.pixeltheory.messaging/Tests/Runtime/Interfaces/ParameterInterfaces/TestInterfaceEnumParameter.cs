using Pixeltheory.Messaging;


public enum TestEnum
{
    One,
    Two,
    Three,
    TestEnumCount
}

[MessagingInterface]
public interface TestInterfaceEnumParameter
{
    void TestMethodEnumParameter(TestEnum enumParam);
}