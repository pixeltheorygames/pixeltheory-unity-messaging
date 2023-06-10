using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceUnityEngineObjectParameter
{
    [MessagingTargetAll] public void TestMethodUnityEngineObjectParameter(UnityEngine.Object unityEngineObjectParameter);
}