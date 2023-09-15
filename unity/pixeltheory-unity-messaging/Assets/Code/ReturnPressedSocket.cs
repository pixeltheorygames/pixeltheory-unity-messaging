using System.Collections.Generic;
using Pixeltheory.Messaging;
using UnityEngine;


public interface IReturnPressed
{
    public void ReturnPressed(float realtimeSinceStartup);
}

[CreateAssetMenu(fileName = "ReturnPressedSocket", menuName = "Assets/Pixeltheory/Data/Sockets/ReturnPressed")]
public class ReturnPressedSocket : PixelSocket<IReturnPressed>
{
    public async void SendMessage()
    {
        await this.PreMessageHooksAsync();
        List<IReturnPressed> listeners;
        if (this.GetListeners(PixelConstantsMessaging.SOCKETS_GLOBAL_CHANNEL, out listeners))
        {
            float timestamp = Time.realtimeSinceStartup;
            for (int i = listeners.Count - 1; i > -1; i--)
            {
                listeners[i].ReturnPressed(timestamp);
            }
        }
        await this.PostMessageHooksAsync();
    }
}
