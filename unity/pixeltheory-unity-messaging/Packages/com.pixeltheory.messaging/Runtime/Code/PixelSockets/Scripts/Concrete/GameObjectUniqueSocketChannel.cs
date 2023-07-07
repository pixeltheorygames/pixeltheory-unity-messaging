using UnityEngine;

public class GameObjectUniqueSocketChannel : MonoBehaviour
{
    #region Fields
    #region Private
    private int uniqueSocketChannel;
    #endregion //Private
    #endregion //Fields

    #region Properties
    #region Public
    public int UniqueSocketChannel => this.uniqueSocketChannel;
    #endregion //Public
    #endregion //Properties
    
    #region Methods
    #region Unity Messages
    private void Awake()
    {
        this.uniqueSocketChannel = this.gameObject.GetInstanceID();
    }
    #endregion
    #endregion
}
