using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DrawingsConfig), menuName = "Configs/" + nameof(DrawingsConfig))]
public class DrawingsConfig : ScriptableObject {
    [field: SerializeField] public List<DrawingData> Drawings { get; private set; }
}
