using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceBoolParameter
{
    [MessagingTargetAll] public void TestMethodBoolParameter(bool boolParam);
}