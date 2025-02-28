using System;
using System.Collections.Generic;
using UnityEngine;


namespace Pixeltheory.Messaging
{
    public abstract class PixelBehaviourSocketed : PixelBehaviour
    {
        #region Fields
        #region Inspector
        [Header("PixelBehaviourSocketed - Messaging Sockets Incoming")]
        [SerializeField] private List<PixelSocket> socketsIncoming;
        [Header("PixelBehaviourSocketed - Messaging Sockets Outgoing")]
        [SerializeField] private List<PixelSocket> socketsOutgoing;
        #endregion //Inspector

        #region Private
        [NonSerialized] private int prefabRootSocketID;
        [NonSerialized] private int gameObjectSocketID;
        [NonSerialized] private int behaviourSocketID;
        #endregion //Private
        #endregion //Fields
        
        #region Properties
        #region Public
        public int UniquePrefabRootSocketID
        {
            get
            {
                if (this.prefabRootSocketID == 0)
                {
                    this.prefabRootSocketID = this.PrefabRootTransform.GetInstanceID();
                }
                return this.prefabRootSocketID;
            }
        }

        public int UniqueGameObjectSocketID
        {
            get
            {
                if (this.gameObjectSocketID == 0)
                {
                    this.gameObjectSocketID = this.gameObject.GetInstanceID();
                }
                return this.gameObjectSocketID;
            }
        }
        
        public int UniqueBehaviourSocketChannel
        {
            get
            {
                if (this.behaviourSocketID == 0)
                {
                    this.behaviourSocketID = this.GetInstanceID();
                }
                return this.behaviourSocketID;
            }
        }
        #endregion //Public
        #endregion //Properties
    }
}
