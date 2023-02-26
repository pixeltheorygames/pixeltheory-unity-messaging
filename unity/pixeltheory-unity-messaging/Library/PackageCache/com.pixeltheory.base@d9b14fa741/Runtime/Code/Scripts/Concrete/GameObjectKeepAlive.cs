using UnityEngine;
using Pixeltheory.Debug;


namespace Pixeltheory
{
    public class GameObjectKeepAlive : MonoBehaviour
    {
        #region Instance
        #region Methods
        #region Unity Messages
        protected virtual void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
            Logging.Log
            (
                "[{0}] Setting as persistent through scene loads/unloads.",
                typeof(GameObjectKeepAlive).FullName
            );
        }
        #endregion //Unity Messages
        #endregion //Methods
        #endregion //Instance
    }   
}