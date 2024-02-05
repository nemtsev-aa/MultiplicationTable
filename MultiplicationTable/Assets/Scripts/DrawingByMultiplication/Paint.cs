using System.Collections.Generic;
using System.IO;
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
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private int _brushSize = 8;

    private Texture2D _texture2;
    private Dictionary<Color, int> _colors = new Dictionary<Color, int>();

    private int _oldRayX, _oldRayY;
    private string _path = "Assets/Textures/Paint.png";

    private void Start() {
        if (_texture == null) 
            _texture = new Texture2D(_textureSize, _textureSize);

        for (int i = 0; i < _textureSize; i++) {
            for (int j = 0; j < _textureSize; j++) {
                _texture.SetPixel(i, j, _color);
            }
        }

        if (_texture.width != _textureSize) 
            _texture.Reinitialize(_textureSize, _textureSize);

        _texture.wrapMode = _textureWrapMode;
        _texture.filterMode = _filterMode;
        _material.mainTexture = _texture;
        
        _texture.Apply();
    }

    //[ContextMenu(nameof(Texture2DToFile))]
    //public void Texture2DToFile() {
    //    byte[] bytes = _texture.EncodeToPNG();
    //    System.IO.File.WriteAllBytes(_path, bytes);
    //    AssetDatabase.ImportAsset(_path);

    //    TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(_path);
    //    importer.isReadable = true;
    //    importer.filterMode = FilterMode.Point;

    //    Debug.Log("Saved to " + _path);
    //}

    [ContextMenu(nameof(SaveTexture2DToFile))]
    void SaveTexture2DToFile() {
        string fromPath = Path.Combine($"{Application.dataPath}/Resources", "Paint.png");
        using (FileStream fs = new FileStream(fromPath, FileMode.Create)) {
            using (BinaryWriter writer = new BinaryWriter(fs)) {
                var bytes = _texture.EncodeToPNG();
                writer.Write(bytes);
                writer.Close();
            }
        }

        Debug.Log($"Save to: {fromPath}");
    }

    [ContextMenu(nameof(LoadImage))]
    private void LoadImage() {

        byte[] imgData;
        
        try {
            imgData = File.ReadAllBytes(Path.Combine($"{Application.dataPath}/Resources", "Paint.png"));
        }
        catch (FileNotFoundException) {
            return;
        }

        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imgData);

        Color32[] colors = texture.GetPixels32();
    }

    [ContextMenu(nameof(ImportTextureFromPNG))]
    public void ImportTextureFromPNG() {
        Color[] colors = _texture2.GetPixels();
        foreach (var iColor in colors) {
            if (_colors.ContainsKey(iColor))
                _colors[iColor] += 1;
            else
                _colors.Add(iColor, 1);
        }

        ShowDictionary();

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

    private void DrawQuad(int rayX, int rayY) {
        for (int y = 0; y < _brushSize; y++) {
            for (int x = 0; x < _brushSize; x++) {
                _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, _color);
            }
        }
    }
    
    private void ShowDictionary() {
        foreach (var iColor in _colors) {
            Debug.Log(iColor);
        }
    }
}
