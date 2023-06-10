using Pixeltheory.Messaging;


namespace TestNamespace
{
    [MessagingInterface]
    public interface TestInterfaceInNamespace
    {
        [MessagingTargetAll] public void TestMethodInNamespaceNamespacedParameter(TestClassWithNamespace.TestClassWithNamespaceEnum testClassWithNamespaceEnum);
        [MessagingTargetAll] public void TestMethodInNamespaceRegularParameter(TestClassNoNamespace.TestClassNoNamespaceEnum testClassNoNamespaceEnum);
    }
}
