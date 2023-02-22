using Pixeltheory;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfacePixelScriptableObjectParameter
{
    [MessagingTargetAll] public void TestMethodOddScriptableObjectParameter(PixelScriptableObject pixelScriptableObjectParameter);
}