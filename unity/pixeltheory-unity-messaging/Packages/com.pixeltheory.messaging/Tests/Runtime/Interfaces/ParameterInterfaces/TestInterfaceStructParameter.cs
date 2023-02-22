using System;
using Pixeltheory.Messaging;


[Serializable]
public struct TestStruct
{
    public int publicIntField;
    public float publicFloatField;
    private char privateCharField;
    public char PublicCharProperty
    {
        get
        {
            return this.privateCharField;
        }
        
        set
        {
            this.privateCharField = value;
        }
    }
    private bool privateBoolField;
    public bool PublicBoolProperty
    {
        get
        {
            return this.privateBoolField;
        }
    }

    public TestStruct(int intParam, float floatParam, char charParam, bool boolParam)
    {
        this.publicIntField = intParam;
        this.publicFloatField = floatParam;
        this.privateCharField = charParam;
        this.privateBoolField = boolParam;
    }

    public void PrintTestStruct()
    {
        string output = "TestStruct : publicIntField = " + this.publicIntField.ToString();
        output += "     publicFloatField = " + this.publicFloatField.ToString();
        output += "     PublicCharProperty = " + this.PublicCharProperty.ToString();
        output += "     PublicBoolProperty = " + this.PublicBoolProperty.ToString();
        //PixeltheoryDebug.EditorLog(output);   
    }
}

[MessagingInterface]
public interface TestInterfaceStructParameter
{
    [MessagingTargetAll] public void TestMethodStructParameter(TestStruct structParam);
}