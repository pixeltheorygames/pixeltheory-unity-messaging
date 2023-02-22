using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceFloatParameter
{
    [MessagingTargetAll] public void TestMethodFloatParameter(float floatParam);
}