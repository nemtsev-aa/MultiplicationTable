using UnityEditor;
using UnityEngine;

public class Paint : MonoBehaviour {
    [Range(2, 512)]
    [SerializeField] private int _textureSize = 16;
    [SerializeField] private TextureWrapMode _textureWrapMode;
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Material _material;

    [SerializeField] private Camera _camera;
    [SerializeField] private Collider _collider;
    [SerializeField] private Color _color;
    [SerializeField] private int _brushSize = 8;

    private int _oldRayX, _oldRayY;

    void OnValidate() {
        if (_texture == null) 
            _texture = new Texture2D(_textureSize, _textureSize);

        if (_texture.width != _textureSize) 
            _texture.Reinitialize(_textureSize, _textureSize);

        _texture.wrapMode = _textureWrapMode;
        _texture.filterMode = _filterMode;
        _material.mainTexture = _texture;
        
        _texture.Apply();
    }

    [ContextMenu(nameof(Texture2DToFile))]
    public void Texture2DToFile() {

        byte[] bytes = _texture.EncodeToPNG();
        string path = "Assets/screenshot.png";
        System.IO.File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path);
        Debug.Log("Saved to " + path);
    }

    private void Update() {

        _brushSize += (int)Input.mouseScrollDelta.y;

        if (Input.GetMouseButton(0)) {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (_collider.Raycast(ray, out hit, 100f)) {

                int rayX = (int)(hit.textureCoord.x * _textureSize);
                int rayY = (int)(hit.textureCoord.y * _textureSize);

                if (_oldRayX != rayX || _oldRayY != rayY) {
                    DrawQuad(rayX, rayY);
                    
                    _oldRayX = rayX;
                    _oldRayY = rayY;
                }
                _texture.Apply();
            }
        }
    }

    void DrawQuad(int rayX, int rayY) {
        for (int y = 0; y < _brushSize; y++) {
            for (int x = 0; x < _brushSize; x++) {
                _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, _color);
            }
        }
    }
}
