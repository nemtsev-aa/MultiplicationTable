using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    [SerializeField] private Texture2D _texture;

    [SerializeField] private Transform _spritesParent;
    [SerializeField] private Sprite _whiteSprite;
    [SerializeField] private Sprite _colorSprite;

    private List<Renderer> _sprites = new List<Renderer>();
    private Dictionary<Color, int> _colors = new Dictionary<Color, int>();


    [ContextMenu(nameof(GetColor))]
    public void GetColor() {
        Color[] colors = _texture.GetPixels();

        foreach (var iColor in colors) {
            if (_colors.ContainsKey(iColor))
                _colors[iColor] += 1;
            else
                _colors.Add(iColor, 0);
        }

        ShowDictionary();
    }

    [ContextMenu("Init")]
    public void Init() {
        CreateSpritesList(_spritesParent);
        CreateColorsDictionary();
        ShowDictionary();
    }

    private void CreateSpritesList(Transform transform) {
        foreach (Transform child in transform) {
            Renderer sprite = child.gameObject.GetComponent<Renderer>();

            _sprites.Add(sprite);
        }
    }

    private void CreateColorsDictionary() {
        foreach (var iRenderer in _sprites) {
            //var iSprite = iSpriteRenderer.sprite;
            var iColor = iRenderer.material.color;

            if (_colors.ContainsKey(iColor))
                _colors[iColor] += 1;
            else
                _colors.Add(iColor, 0);
        }
    }

    private void ShowDictionary() {
        foreach (var iColor in _colors) {
            Debug.Log(iColor);
        }
    }
}
