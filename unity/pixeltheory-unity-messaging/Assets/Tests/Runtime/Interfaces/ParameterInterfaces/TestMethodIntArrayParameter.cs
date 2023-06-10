using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceArrayParameter
{
    [MessagingTargetAll] public void TestMethodIntArrayParameter(int[] arrayParam);
}