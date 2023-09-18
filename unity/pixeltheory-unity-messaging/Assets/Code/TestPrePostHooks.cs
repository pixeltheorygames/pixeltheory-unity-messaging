using System.Threading.Tasks;
using Pixeltheory.Debug;
using Pixeltheory.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPrePostHooks : MonoBehaviour,
    IReturnPressed,
    ISpacebarPressed
{
    [SerializeField] private ReturnPressedSocket returnPressedSocket;
    [SerializeField] private SpacebarPressedSocket spacebarPressedSocket;

    private TaskCompletionSource<bool> spacebarPressedSignal;

    private void OnEnable()
    {
        this.returnPressedSocket.Bind(this, 0);
        this.spacebarPressedSocket.Bind(this, 0);
    }

    private void Start()
    {
        this.returnPressedSocket.AttachMessageHook
        (
            PixelSocket<IReturnPressed>.PixelSocketHookTypeFlag.PreMessageHook,
            this.WaitForSpacebar
        );
        this.returnPressedSocket.AttachMessageHook
        (
            PixelSocket<IReturnPressed>.PixelSocketHookTypeFlag.PostMessageHook,
            this.RemovePreHook
        );
    }

    private void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            this.returnPressedSocket.SendMessage();
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            this.spacebarPressedSocket.SendMessage();
        }
    }

    private void OnDisable()
    {
        this.spacebarPressedSocket.Unbind(this, 0);
        this.returnPressedSocket.Unbind(this, 0);
    }

    public void ReturnPressed(float realtimeSinceStartup)
    {
        PixelLog.Log("Return key pressed.");
    }
    
    public void SpacebarPressed(float realtimeSinceStartup)
    {
        PixelLog.Log("Spacebar key pressed.");
        if (!this.spacebarPressedSignal.Task.IsCompleted)
        {
            this.spacebarPressedSignal.SetResult(true);   
        }
        else
        {
            this.spacebarPressedSocket.Unbind(this, 0);
        }
    }

    private Task WaitForSpacebar()
    {
        this.spacebarPressedSignal = new TaskCompletionSource<bool>();
        return this.spacebarPressedSignal.Task;
    }

    private Task RemovePreHook()
    {
        this.returnPressedSocket.RemoveMessageHook
        (
            PixelSocket<IReturnPressed>.PixelSocketHookTypeFlag.PreMessageHook,
            this.WaitForSpacebar
        );
        this.returnPressedSocket.RemoveMessageHook
        (
            PixelSocket<IReturnPressed>.PixelSocketHookTypeFlag.PostMessageHook,
            this.RemovePreHook
        );
        return Task.CompletedTask;
    }
}
