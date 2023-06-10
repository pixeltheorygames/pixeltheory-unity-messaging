using System;
using UnityEngine;
using Pixeltheory.Messaging;


[Serializable]
public class TestClass
{
    #region Fields
    #region Private
    [SerializeField] private int testClassInt;
    [SerializeField] private float testClassFloat;
    #endregion //Private
    #endregion //Fields

    #region Methods
    #region Public
    public override string ToString()
    {
        return String.Format
        (
            "TestClass.testClassInt = {0}, TestClass.testClassFloat = {1}",
            this.testClassInt.ToString(),
            this.testClassFloat.ToString("R")
        );
    }
    #endregion //Public
    #endregion //Methods
}


[MessagingInterface]
public interface TestInterfaceClassParameter
{
    [MessagingTargetAll] public void TestMethodClassParameter(TestClass classPrameter);
}