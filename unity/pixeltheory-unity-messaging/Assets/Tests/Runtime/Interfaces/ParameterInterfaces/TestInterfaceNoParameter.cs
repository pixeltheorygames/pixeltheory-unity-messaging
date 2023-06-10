using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceNoParameter
{
    [MessagingTargetAll] public void TestMethodNoParameter();
}