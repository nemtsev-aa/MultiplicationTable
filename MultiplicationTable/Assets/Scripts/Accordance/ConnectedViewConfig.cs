using UnityEngine;

[CreateAssetMenu(fileName = nameof(ConnectedViewConfig), menuName = "Configs/" + nameof(ConnectedViewConfig))]
public class ConnectedViewConfig : ScriptableObject {
    [field: SerializeField] public Color DefaultColor { get; private set; }
    [field: SerializeField] public Color SelectionColor { get; private set; } = Color.blue;
    [field: SerializeField] public Color TrueVerificationColor { get; private set; } = Color.green;
    [field: SerializeField] public Color FalseVerificationColor { get; private set; } = Color.red;
}
