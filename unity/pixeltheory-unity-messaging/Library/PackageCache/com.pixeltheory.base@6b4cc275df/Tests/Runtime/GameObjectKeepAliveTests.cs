using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


namespace Pixeltheory.Tests
{
    public class GameObjectKeepAliveTests
    {
        [UnityTest]
        public IEnumerator GameObjectKeepAliveSceneLoadTest()
        {
            GameObject testGameObject =
                new GameObject
                (
                    "Test GameObject with GameObjectKeepAlive",
                    typeof(GameObjectKeepAlive)
                );
            AsyncOperation sceneLoader = 
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            sceneLoader.allowSceneActivation = true;
            while (!sceneLoader.isDone)
            {
                yield return null;
            }
            Assert.IsTrue(testGameObject != null);
        }
    }
}
