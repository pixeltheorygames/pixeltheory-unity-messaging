using System;
using Pixeltheory.Debug;
using UnityEngine;
using UnityEngine.Assertions;

using Object = UnityEngine.Object;


namespace Pixeltheory
{
    public abstract class PixelBehaviour<TypePixelBehaviour> : MonoBehaviour where TypePixelBehaviour : PixelBehaviour<TypePixelBehaviour>
    {
        #region Class
        #region Fields
        #region Private
        private static TypePixelBehaviour instance = null;
        private static Boolean gameIsShuttingDown = false;
        #endregion //Private
        #endregion //Fields
        #endregion //Class

        #region Instance
        #region Fields
        #region Inspector
        [Header("PixelBehaviour Single Instance")] 
        [SerializeField] private bool onlyAllowSingleInstance = false;
        #endregion //Inspector

        #region Protected
        protected bool isBeingDestroyed = false;
        #endregion //Protected
        #endregion //Fields
        
        #region Methods
        #region Unity Messages
        protected virtual void Awake()
        {
            if (this.onlyAllowSingleInstance)
            {
                string className = typeof(TypePixelBehaviour).FullName;
                bool setNewInstance = 
                    !PixelBehaviour<TypePixelBehaviour>.gameIsShuttingDown &&
                    PixelBehaviour<TypePixelBehaviour>.instance == null || 
                    (PixelBehaviour<TypePixelBehaviour>.instance.GetInstanceID() != this.GetInstanceID() 
                        && PixelBehaviour<TypePixelBehaviour>.instance.isBeingDestroyed);
                if (setNewInstance)
                {
                    PixelBehaviour<TypePixelBehaviour>.instance = this as TypePixelBehaviour;
                    Logging.Log("[{0}] Setting first instance as single instance.", className);
                }
                else
                {
                    this.isBeingDestroyed = true;
                    if (Application.isEditor && !Application.isPlaying)
                    {
                        GameObject.DestroyImmediate(this as Object);
                    }
                    else
                    {
                        GameObject.Destroy(this as Object);
                    }
                    Logging.Warn("[{0}] Instance already exists; destroying self.", className);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            this.isBeingDestroyed = true;
            if (this.onlyAllowSingleInstance && PixelBehaviour<TypePixelBehaviour>.instance == this as TypePixelBehaviour)
            {
                PixelBehaviour<TypePixelBehaviour>.instance = null;
                Logging.Log("[{0}] Singleton instance is being destroyed.", typeof(TypePixelBehaviour).FullName);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            PixelBehaviour<TypePixelBehaviour>.gameIsShuttingDown = true;
        }
        #endregion //Unity Messages
        #endregion //Methods
        #endregion //Instance
    }
    
    public abstract class PixelBehaviour<TypePixelBehaviour, TypeRuntimeData> : PixelBehaviour<TypePixelBehaviour> 
        where TypePixelBehaviour : PixelBehaviour<TypePixelBehaviour>
        where TypeRuntimeData : PixelRuntimeData<TypeRuntimeData>
    {
        #region Instance
        #region Fields
        #region Inspector
        [Header("PixelBehaviour Data Injection")]
        [SerializeField] private DataManager<TypeRuntimeData> runtimeDataInjector;
        #endregion //Inspector
    
        #region Protected
        protected TypeRuntimeData runtimeData;
        #endregion //Protected
        #endregion //Fields
    
        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            Assert.IsNotNull( this.runtimeDataInjector );
            
            base.Awake();
            if (!this.isBeingDestroyed)
            {
                this.runtimeData = this.runtimeDataInjector.GetData();   
            }
        }
    
        protected override void OnDestroy()
        {
            this.runtimeData = null;
            base.OnDestroy();
        }
        #endregion //Unity Messages
        #endregion //Methods
        #endregion //Instance
    }
}
