using System.Collections.Generic;
using Pixeltheory.Messaging;
using UnityEngine;

public interface ISpacebarPressed
{
    public void SpacebarPressed(float realtimeSinceStartup);
}

[CreateAssetMenu(fileName = "SpacebarPressedSocket", menuName = "Assets/Pixeltheory/Data/Sockets/SpacebarPressed")]
public class SpacebarPressedSocket : PixelSocket<ISpacebarPressed>
{
    public async void SendMessage()
    {
        await this.PreMessageHooksAsync();
        List<ISpacebarPressed> listeners;
        if (this.GetListeners(PixelConstantsMessaging.SOCKETS_GLOBAL_CHANNEL, out listeners))
        {
            float timestamp = Time.realtimeSinceStartup;
            for (int i = listeners.Count - 1; i > -1; i--)
            {
                listeners[i].SpacebarPressed(timestamp);
            }
        }
        await this.PostMessageHooksAsync();
    }
}
