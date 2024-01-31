using System;
using UnityEngine;

[Serializable]
public class DrawingData {
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Texture2D Texture { get; private set; }
}
