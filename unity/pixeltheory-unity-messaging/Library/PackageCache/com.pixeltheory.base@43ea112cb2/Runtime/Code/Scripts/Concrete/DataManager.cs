using System;
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
        private void Start()
        {
            TypeRuntimeData[] runtimeDataInstances =
                UnityEngine.Object.FindObjectsOfType(typeof(TypeRuntimeData)) as TypeRuntimeData[];
            if (runtimeDataInstances == null)
            {
                this.runtimeData = ScriptableObject.CreateInstance<TypeRuntimeData>();
            }
            else
            {
                switch (runtimeDataInstances.Length)
                {
                    case 0:
                        this.runtimeData = ScriptableObject.CreateInstance<TypeRuntimeData>();
                        break;
                    case 1:
                        this.runtimeData = runtimeDataInstances[0];
                        break;
                    default:
                        Logging.Warn
                        (
                            "Multiple instances of {0} found. Keeping oldest instance and killing all others.",
                            typeof(TypeRuntimeData)
                        );
                        float earliestTimestamp = float.MaxValue;
                        foreach (TypeRuntimeData data in runtimeDataInstances)
                        {
                            if (data.CreationTimestamp < earliestTimestamp)
                            {
                                this.runtimeData = data;
                            }
                            else
                            {
                                UnityEngine.Object.Destroy(data);
                            }
                        }
                        break;
                }
            }
            if (this.runtimeDataOverride != null)
            {
                this.runtimeDataOverride.CopyTo(this.runtimeData);
            }
            Object.DontDestroyOnLoad(this.runtimeData);
            this.runtimeData.hideFlags = HideFlags.DontSave;
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