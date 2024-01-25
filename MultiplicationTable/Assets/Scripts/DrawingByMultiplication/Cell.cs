using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Cell : UICompanent {
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _textLabel;

    private Vector2 _position;
    private string _textValue;

    public Vector2 Position {
        get { 
            return _position;
        }

        set {
            if (value.x < 0 || value.y < 0) 
                throw new ArgumentOutOfRangeException($"Invalid Position value: {value}");
            
            _position = value;
            transform.position = _position;
        }
    }

    public string TextValue {
        get { 
            return _textValue;
        }

        set {
            _textValue = value;
            _textLabel.text = _textValue;
        }
    }

    public Color MainColor { get; set; }
    
    public Color BackColor { get; set; } = Color.white;

    public void ShowColor(bool status) {
        if (status == true)
            _background.color = MainColor;

        _background.color = BackColor;
    }

}
