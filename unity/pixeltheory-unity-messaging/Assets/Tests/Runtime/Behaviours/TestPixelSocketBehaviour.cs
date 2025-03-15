using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pixeltheory.Debug;
using Pixeltheory.Messaging;
using UnityEngine;


namespace Pixeltheory.Messaging.Tests
{
	public class TestPixelSocketBehaviour : PixelSocketBehaviour
	{
		#region Fields
		#region Private
		private TestPixelSocket testPixelSocket;
		#endregion //Private
		#endregion //Fields
		
		#region Methods
		#region Unity Messages
		protected override void Awake()
		{
			base.Awake();
			this.testPixelSocket = this.pixelSocketsIncoming[0].Key as TestPixelSocket;
		}

		private async void Start()
		{
			this.testPixelSocket.TestInt = 1;
			await Task.Delay(1000);
			//this.blackboardModuleSocketSwitch.Broadcast(this.testPixelSocket);
			this.testPixelSocket.TestInt = 10;
			await Task.Delay(1000);
			//this.blackboardModuleSocketSwitch.Broadcast(this.testPixelSocket);
			this.testPixelSocket.TestInt = 100;
			await Task.Delay(1000);
			//this.blackboardModuleSocketSwitch.Broadcast(this.testPixelSocket);
			this.testPixelSocket.TestInt = 1000;
			await Task.Delay(1000);
			//this.blackboardModuleSocketSwitch.Broadcast(this.testPixelSocket);
		}
		#endregion //Unity Messages
		
		#region Socket Signal Handlers
		public Task TestPixelSocketSignal()
		{
			PixelLog.Log(this.testPixelSocket.TestInt);
			return Task.CompletedTask;
		}
		#endregion //Socket Signal Handlers
		#endregion //Methods
	}
}