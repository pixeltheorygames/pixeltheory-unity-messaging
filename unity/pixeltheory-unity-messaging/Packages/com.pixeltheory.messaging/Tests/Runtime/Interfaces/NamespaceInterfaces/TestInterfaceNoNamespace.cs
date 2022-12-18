using Pixeltheory.Messaging;
using TestNamespace;


[MessagingInterface]
public interface TestInterfaceNoNamespace
{
    void TestMethodNoNamespaceNamespacedParameter(TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum);
    void TestMethodNoNamespaceRegularParameter(TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum);
}
