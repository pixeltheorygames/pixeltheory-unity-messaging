using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Pixeltheory.Tests
{
    public class PixelBehaviourTests
    {
        [UnityTest]
        public IEnumerator RuntimeExistenceTest()
        {
            GameObject testGameObjectOne =
                new GameObject
                (
                    "Test GameObject with PixelBehaviourSingleTest",
                    typeof(PixelBehaviourSingleTest)
                );
            yield return null;
            GameObject testGameObjectTwo =
                new GameObject
                (
                    "Test GameObject without PixelBehaviourSingleTest",
                    typeof(PixelBehaviourSingleTest)
                );
            yield return null;
            PixelBehaviourSingleTest testPixelBehaviourSingleOne =
                testGameObjectOne.GetComponent<PixelBehaviourSingleTest>();
            PixelBehaviourSingleTest testPixelBehaviourSingleTwo =
                testGameObjectTwo.GetComponent<PixelBehaviourSingleTest>();
            Assert.IsTrue((testPixelBehaviourSingleOne != null) && (testPixelBehaviourSingleTwo == null));
        }

        private class PixelBehaviourSingleTest : PixelBehaviour<PixelBehaviourSingleTest>, IMonoBehaviourTest
        {
            public bool IsTestFinished => true;
        }
    }
}
