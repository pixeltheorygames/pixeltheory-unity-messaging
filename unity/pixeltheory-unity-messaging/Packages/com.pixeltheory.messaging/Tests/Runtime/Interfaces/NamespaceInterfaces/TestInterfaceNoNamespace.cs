using Pixeltheory.Messaging;
using TestNamespace;


[MessagingInterface]
public interface TestInterfaceNoNamespace
{
    [MessagingTargetAll] public void TestMethodNoNamespaceNamespacedParameter(TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum);
    [MessagingTargetAll] public void TestMethodNoNamespaceRegularParameter(TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum);
}
