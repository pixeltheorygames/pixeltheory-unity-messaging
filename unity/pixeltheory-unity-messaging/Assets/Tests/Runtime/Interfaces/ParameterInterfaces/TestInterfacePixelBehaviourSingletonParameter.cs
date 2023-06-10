using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfacePixelBehaviourSingletonParameter
{
    [MessagingTargetAll] public void TestMethodOddBehaviourSingletonParameter(TestClassPixelBehaviourSingleton pixelBehaviourSingletonParameter);
}