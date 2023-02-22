using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceQuaternionParameter
{
    [MessagingTargetAll] public void TestMethodQuaternionParameter(Quaternion quaternionParam);
}