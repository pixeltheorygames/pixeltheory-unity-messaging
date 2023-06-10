using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceCharParameter
{
    [MessagingTargetAll] public void TestMethodCharParameter(char charParam);
}