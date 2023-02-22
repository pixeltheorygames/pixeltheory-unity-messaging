using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceGameObjectParameter
{
    [MessagingTargetAll] public void TestMethodGameObjectParameter(GameObject gameObjectParameter);
}