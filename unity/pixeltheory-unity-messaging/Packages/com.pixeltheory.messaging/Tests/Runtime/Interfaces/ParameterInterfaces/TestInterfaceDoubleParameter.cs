using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceDoubleParameter
{
    [MessagingTargetAll] public void TestMethodDoubleParameter(double doubleParam);
}