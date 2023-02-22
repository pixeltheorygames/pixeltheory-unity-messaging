using UnityEngine;
using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceScriptableObjectParameter
{
    [MessagingTargetAll] public void TestMethodScriptableObjectParameter(ScriptableObject scriptableObjectParameter);
}