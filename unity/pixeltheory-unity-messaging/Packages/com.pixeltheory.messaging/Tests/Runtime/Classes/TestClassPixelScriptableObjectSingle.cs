using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Pixeltheory;
using UnityEngine;

[Serializable]
public class TestClassPixelScriptableObjectSingle : PixelScriptableObject
{
    [SerializeField] private int[] integers;
    [SerializeField] private List<GameObject> gameObjects;
}
