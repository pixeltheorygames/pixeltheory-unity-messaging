namespace Pixeltheory.Messaging.Tests
{
    [MessagingInterface]
    public interface ITestMessagingTargetSingle
    {
        [MessagingTargetSingle] public void TestMessagingTargetSingleInt(float realtimeSinceStartup, int testInt);
    }
}
