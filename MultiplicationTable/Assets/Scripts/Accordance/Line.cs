using UnityEngine;

public class Line : MonoBehaviour {
    private Material _material;

    [field: SerializeField] public LineRenderer Renderer { get; private set; }
    public Vector3 StartPoint => Renderer.GetPosition(0);
    

    public void Init() {
        _material = new Material(Renderer.material);
        Renderer.material = _material;

        Renderer.positionCount = 1;
    }

    public void SetColor(Color color) => _material.color = color;

}
