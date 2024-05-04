using Pixeltheory.Blackboard;
using Pixeltheory.Debug;
using UnityEngine;


[CreateAssetMenu(fileName = "TestRuntimeData", menuName = "Pixeltheory/Data/Blackboard/TestRuntimeData")]
public class TestRuntimeData : PixelBlackboardData
{
	public override void OnBlackboardLoad()
	{
		PixelLog.Log("TestRuntimeData - OnBlackboardDataLoad");
	}
	
	public override void OnBlackboardUnload()
	{
		PixelLog.Log("TestRuntimeData - OnBlackboardDataUnload");
	}
}
