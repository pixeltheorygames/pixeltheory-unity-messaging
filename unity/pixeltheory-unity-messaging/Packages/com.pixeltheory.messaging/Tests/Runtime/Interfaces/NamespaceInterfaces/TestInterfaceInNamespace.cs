using Pixeltheory.Messaging;


namespace TestNamespace
{
    [MessagingInterface]
    public interface TestInterfaceInNamespace
    {
        void TestMethodInNamespaceNamespacedParameter(TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum);
        void TestMethodInNamespaceRegularParameter(TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum);
    }
}
