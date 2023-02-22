using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceInheritance : TestInterfaceValueTypes, TestInterfaceReferenceTypes
{
    [MessagingTargetAll] public void TestMethodInheritance(Quaternion quaternionParam);
}
