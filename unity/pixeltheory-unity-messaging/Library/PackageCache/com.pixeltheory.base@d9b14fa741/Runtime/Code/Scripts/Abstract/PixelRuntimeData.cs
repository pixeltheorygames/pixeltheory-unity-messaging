using System;
using UnityEngine;


namespace Pixeltheory
{
    [Serializable]
    public abstract class PixelRuntimeData<TypeRuntimeData> : PixelScriptableObject
    {
        #region Fields
        #region Protected
        protected float creationTimestamp;
        #endregion //Protected

        #region Public
        public float CreationTimestamp => this.creationTimestamp;
        #endregion //Public
        #endregion //Fields

        #region Methods
        #region Unity Messages
        public virtual void OnEnable()
        {
            if (Application.isPlaying)
            {
                this.creationTimestamp = Time.realtimeSinceStartup;
            }
        }
        #endregion //Unity Messages
        #endregion //Methods
    }
}