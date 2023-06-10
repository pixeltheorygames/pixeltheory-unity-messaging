using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceMonoBehaviourParameter
{
    [MessagingTargetAll] public void TestMethodMonoBehaviourParameter(MonoBehaviour monoBehaviourParameter);
}