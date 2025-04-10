using UnityEngine;


namespace Pixeltheory.Messaging.Tests
{
	[CreateAssetMenu(fileName = "TestStringPixelSocket", menuName = "Pixeltheory/Messaging/Tests/TestStringPixelSocket")]
	public class TestStringPixelSocket : PixelSocket<TestStringPixelSocket>
	{
		#region Properties
		#region Public
		public string TestString;
		#endregion //Public
		#endregion //Properties
	}
}