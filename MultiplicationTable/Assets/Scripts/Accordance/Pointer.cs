using UnityEngine;

public class Pointer : MonoBehaviour {
    [SerializeField] private Transform _sprite;

    private Camera _palyerCamera;

    private void Start() {
        _palyerCamera = Camera.main;
    }

    private void Update() {
        Ray ray = _palyerCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-Vector3.forward, Vector3.zero);

        if (plane.Raycast(ray, out float distance)) {
            Vector3 point = ray.GetPoint(distance);
            _sprite.position = point;
        }
    }
}
