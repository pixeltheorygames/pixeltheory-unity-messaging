using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pixeltheory.Messaging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Random = UnityEngine.Random;


namespace Pixeltheory.Blackboard.Modules
{
    [CreateAssetMenu(fileName = "PixelBlackboardModuleSocketSwitch", menuName = "Pixeltheory/Blackboard/Modules/SocketSwitch")]
    public class PixelBlackboardModuleSocketSwitch : PixelBlackboardModule
    {
        #region Fields
        #region Public
        public const string moduleKey = "BlackboardModuleSocketSwitch";
        #endregion //Public

        #region Inspector
        [SerializeField] private List<PixelSocket> pixelSocketsList;
        #endregion //Inspector

        #region Private
        [NonSerialized] private Dictionary<PixelSocket,UnityEvent> socketsToListenersDictionary;
        #endregion //Private
        #endregion //Fields

        #region Properties
        #region PixelBlackboardModule Overrides
        public override string ModuleKey => PixelBlackboardModuleSocketSwitch.moduleKey;
        #endregion //PixelBlackboardModule Overrides
        #endregion //Properties

        #region Methods
        #region PixelBlackboardModule Overrides
        public override void OnBlackboardLoad(PixelBlackboard blackboard)
        {
            this.socketsToListenersDictionary = new Dictionary<PixelSocket, UnityEvent>();
            foreach (PixelSocket pixelSocket in this.pixelSocketsList)
            {
                this.socketsToListenersDictionary.Add(pixelSocket, new UnityEvent());
            }
        }

        public override void OnBlackboardUnload(PixelBlackboard blackboard)
        {
            foreach (UnityEvent listeners in this.socketsToListenersDictionary.Values)
            {
                listeners.RemoveAllListeners();
            }
            this.socketsToListenersDictionary.Clear();
            this.socketsToListenersDictionary = null;
        }
        #endregion //PixelBlackboardModule Overrides

        #region Public
        public void RegisterSocket(PixelSocket socket, int messagingID, UnityAction listener)
        {
            if (!this.socketsToListenersDictionary.ContainsKey(socket))
            {
                this.socketsToListenersDictionary.Add(socket, new UnityEvent());
            }
            this.socketsToListenersDictionary[socket].AddListener(listener);
        }

        public void DeregisterSocket(PixelSocket socket, int messagingID, UnityAction listener)
        {
            if (this.socketsToListenersDictionary.ContainsKey(socket))
            {
                this.socketsToListenersDictionary[socket].RemoveListener(listener);
            }
        }

        // public async void Broadcast(PixelSocket socket)
        // {
        //     foreach (UnityEvent listeners in this.socketsToListenersDictionary[socket])
        //     {
        //         await listeners.Invoke();
        //     }
        // }
        //
        // public async void Unicast(PixelSocket socket, int targetID)
        // {
        //     foreach ((int, UnityAction<Task>) listener in this.listeners[socket])
        //     {
        //         if (listener.Item1 == targetID)
        //         {
        //             await listener.Item2.Invoke();
        //         }
        //     }
        // }
        //
        // public async void Multicast(PixelSocket socket, HashSet<int> targetIDSet)
        // {
        //     foreach ((int, UnityAction<Task>) listener in this.listeners[socket])
        //     {
        //         if (targetIDSet.Contains(listener.Item1))
        //         {
        //             await listener.Item2.Invoke();
        //         }
        //     }
        // }
        //
        // public async void Anycast(PixelSocket socket)
        // {
        //     if (this.listeners[socket].Count > 0)
        //     {
        //         int randomIndex = Random.Range(0, this.listeners[socket].Count - 1);
        //         await this.listeners[socket][randomIndex].Item2.Invoke();
        //     }
        // }
        #endregion //Public
        #endregion //Methods
    }
}