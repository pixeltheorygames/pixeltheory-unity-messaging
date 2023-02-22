namespace Pixeltheory.Messaging.Tests
{
    [MessagingInterface]
    public interface ITestMessagingTargetAll
    {
        [MessagingTargetAll] public void TestMessagingTargetAllInt(float realtimeSinceStartup, int testInt);
    }
}