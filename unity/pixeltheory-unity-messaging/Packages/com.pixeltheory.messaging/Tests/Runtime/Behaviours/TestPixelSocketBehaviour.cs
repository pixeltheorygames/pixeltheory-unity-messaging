using System;
using System.Threading;
using Pixeltheory.Debug;
using UnityEngine;


namespace Pixeltheory.Messaging.Tests
{
	public class TestPixelSocketBehaviour : PixelSocketBehaviour
	{
		#region Fields
		#region Private
		[SerializeField] private TestIntPixelSocket testIntPixelSocket;
		[SerializeField] private TestStringPixelSocket testStringPixelSocket;
		[SerializeField] private bool temp;
		#endregion //Private
		#endregion //Fields
		
		#region Methods
		#region Unity Messages
		protected override void OnEnable()
		{
			this.testIntPixelSocket.AddListener(this.TestIntPixelSocketBroadcast, this.GetType(), this.UnicastID, this.destroyCancellationToken);
			this.testIntPixelSocket.AddListener(this.TestIntPixelSocketBroadcast2, this.GetType(), this.UnicastID, this.destroyCancellationToken);
			this.testIntPixelSocket.AddListener(this.TestIntPixelSocketBroadcast3, this.GetType(), this.UnicastID, this.destroyCancellationToken);
			this.testStringPixelSocket.AddListener(this.TestStringPixelSocketBroadcast, this.GetType(), this.UnicastID, this.destroyCancellationToken);
			this.testStringPixelSocket.AddListener(this.TestStringPixelSocketBroadcast2, this.GetType(), this.UnicastID, this.destroyCancellationToken);
		}
		
		protected override void OnDisable()
		{
			this.testIntPixelSocket.RemoveListener(this.TestIntPixelSocketBroadcast, this.GetType(), this.UnicastID, this.destroyCancellationToken);
			this.testStringPixelSocket.RemoveListener(this.TestStringPixelSocketBroadcast, this.GetType(), this.UnicastID, this.destroyCancellationToken);
		}
		#endregion //Unity Messages

		#region Socket Messages
		private async Awaitable TestIntPixelSocketBroadcast(float timestamp, TestIntPixelSocket socket, CancellationToken ct)
		{
			await Awaitable.WaitForSecondsAsync(5.0f, ct);
			PixelLog.Log("TestIntPixelSocket broadcast received with int data {0}.", socket.TestInt);
		}

		private async Awaitable TestIntPixelSocketBroadcast2(float timestamp, TestIntPixelSocket socket, CancellationToken ct)
		{
			await Awaitable.WaitForSecondsAsync(5.0f, ct);
			PixelLog.Log("TestIntPixelSocket2 broadcast received with int data {0}.", socket.TestInt);
		}
		
		private async Awaitable TestIntPixelSocketBroadcast3(float timestamp, TestIntPixelSocket socket, CancellationToken ct)
		{
			await Awaitable.WaitForSecondsAsync(5.0f, ct);
			PixelLog.Log("TestIntPixelSocket3 broadcast received with int data {0}.", socket.TestInt);
		}
		
		private async Awaitable TestStringPixelSocketBroadcast(float timestamp, TestStringPixelSocket socket, CancellationToken ct)
		{
			PixelLog.Log("TestStringPixelSocket broadcast received with string data {0}.", socket.TestString);
			await Awaitable.EndOfFrameAsync(ct);
		}
		
		private async Awaitable TestStringPixelSocketBroadcast2(float timestamp, TestStringPixelSocket socket, CancellationToken ct)
		{
			await Awaitable.NextFrameAsync(ct);
			throw new Exception("Test exception");
		}
		#endregion //Socket Messages
		#endregion //Methods
	}
}