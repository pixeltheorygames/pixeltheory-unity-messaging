using UnityEngine;
using UnityEditor;


namespace Pixeltheory.Editor
{
    public class SceneCameraTool
    {
        #region Class
        #region Methods
        [MenuItem("GameObject/Copy From Scene Camera Coordinates", false, -1002)]
        private static void CopyFromSceneCameraTransform(MenuCommand command)
        {
            SceneView.lastActiveSceneView.AlignWithView();
        }
        
        [MenuItem("GameObject/Copy To Scene Camera Coordinates", false, -1001)]
        private static void CopyToSceneCameraTransform(MenuCommand command)
        {
            GameObject gameObjectToCopyFrom = command.context as GameObject;
            SceneView.lastActiveSceneView.AlignViewToObject(gameObjectToCopyFrom.transform);
        }
        #endregion //Methods
        #endregion //Class
    }
}