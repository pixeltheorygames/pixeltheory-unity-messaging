using Pixeltheory.Messaging;


[MessagingInterface]
public interface TestInterfaceSimpleTypes :
    TestInterfaceByteParameter, TestInterfaceShortParameter, TestInterfaceIntParameter, 
    TestInterfaceLongParameter, TestInterfaceCharParameter, TestInterfaceFloatParameter, 
    TestInterfaceDoubleParameter, TestInterfaceBoolParameter
{
   
}