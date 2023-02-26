using System;
using System.Collections.Generic;
using Pixeltheory.Debug;
using UnityEngine;

using Object = UnityEngine.Object;


namespace Pixeltheory
{
    public static class DataManager
    {
        #region Class
        public const int DataManagerExecutionOrder = Int32.MinValue;
        #endregion //Class
    }
    
    [DefaultExecutionOrder(DataManager.DataManagerExecutionOrder)]
    [DisallowMultipleComponent]
    public class DataManager<TypeRuntimeData> : PixelBehaviour<DataManager<TypeRuntimeData>>
        where TypeRuntimeData : PixelRuntimeData<TypeRuntimeData>
    {
        #region Instance
        #region Fields
        #region Inspector
        [SerializeField] private TypeRuntimeData runtimeDataOverride;
        #endregion //Inspector

        #region Private
        private TypeRuntimeData runtimeData;
        #endregion //Private
        #endregion //Fields

        #region Methods
        #region Unity Messages
        protected override void Awake()
        {
            base.Awake();
            TypeRuntimeData[] runtimeDataInstances =
                UnityEngine.Object.FindObjectsOfType(typeof(TypeRuntimeData)) as TypeRuntimeData[];
            this.runtimeData = this.runtimeDataOverride;
            this.runtimeData = this.runtimeData == null ? PixelRuntimeData<TypeRuntimeData>.CreateInstance<TypeRuntimeData>() : this.runtimeData;
            float currentEarliestTimestamp = this.runtimeData == null ? float.MaxValue : this.runtimeData.CreationTimestamp;
            List<TypeRuntimeData> dataInstancesToDestroy = new List<TypeRuntimeData>();
            foreach (TypeRuntimeData dataInstance in runtimeDataInstances)
            {
                if (dataInstance.CreationTimestamp < currentEarliestTimestamp)
                {
                    dataInstancesToDestroy.Add(this.runtimeData);
                    this.runtimeData = dataInstance;
                    currentEarliestTimestamp = dataInstance.CreationTimestamp;
                }
                else
                {
                    dataInstancesToDestroy.Add(dataInstance);
                }
            }
            this.runtimeData.hideFlags = HideFlags.DontSave;
            UnityEngine.Object.DontDestroyOnLoad(this.runtimeData);
            if (dataInstancesToDestroy.Count > 0)
            {
                Logging.Warn
                (
                    "Multiple instances of {0} found. Keeping oldest instance and killing all others.",
                    typeof(TypeRuntimeData)
                );
                foreach (TypeRuntimeData destroyingDataInstance in dataInstancesToDestroy)
                {
                    UnityEngine.Object.Destroy(destroyingDataInstance);
                }
            }
        }
        #endregion Unity Messages

        #region Public
        public TypeRuntimeData GetData()
        {
            return this.runtimeData;
        }
        #endregion //Public
        #endregion //Methods
        #endregion //Instance
    }
}