using Pixeltheory.Blackboard;
using Pixeltheory.Debug;
using UnityEngine;


[CreateAssetMenu(fileName = "TestRuntimeData", menuName = "Pixeltheory/Data/Blackboard/TestRuntimeData")]
public class TestRuntimeData : PixelBlackboardModule
{
	public override void OnBlackboardLoad(PixelBlackboard blackboard)
	{
		PixelLog.Log("TestRuntimeData - OnBlackboardDataLoad");
	}
	
	public override void OnBlackboardUnload(PixelBlackboard blackboard)
	{
		PixelLog.Log("TestRuntimeData - OnBlackboardDataUnload");
	}
}
