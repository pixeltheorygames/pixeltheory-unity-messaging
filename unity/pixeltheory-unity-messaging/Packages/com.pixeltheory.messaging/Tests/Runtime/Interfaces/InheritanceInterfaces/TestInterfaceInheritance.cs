using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceInheritance : TestInterfaceValueTypes, TestInterfaceReferenceTypes
{
    void TestMethodInheritance(Quaternion quaternionParam);
}
