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
    [MessagingTargetAll] public void TestMethodEnumParameter(TestEnum enumParam);
}