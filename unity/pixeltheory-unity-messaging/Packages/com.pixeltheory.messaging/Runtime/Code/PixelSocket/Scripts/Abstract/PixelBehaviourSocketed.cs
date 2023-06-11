using Pixeltheory;
using UnityEngine;


public abstract class PixelBehaviourSocketed<TypeSelf, TypeData> : PixelBehaviour<TypeSelf, TypeData>
    where TypeSelf : PixelBehaviourSocketed<TypeSelf, TypeData>
    where TypeData : PixelObject
{
    #region Fields
    #region Inspector
    [Header("PixelBehaviourSocketed")]
    [SerializeField] private GameObject prefabRootGameObject;
    #endregion //Inspector
    #endregion //Fields

    #region Properties
    #region Protected
    protected int UniqueSocketChannel => this.prefabRootGameObject?.GetInstanceID() ?? this.gameObject.GetInstanceID();
    #endregion //Protected
    #endregion //Properties
}
