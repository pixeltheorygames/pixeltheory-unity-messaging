using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceStringParameter
{
    [MessagingTargetAll] public void TestMethodStringParameter(string stringParam);
}
