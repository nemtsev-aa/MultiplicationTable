using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

[Serializable]
public struct CellStateConfig {
    public CellStateConfig(Sprite sprite, Color color) {
        Sprite = sprite;
        Color = color;
    }

    [field: SerializeField] public Color Color { get; set; }
    [field: SerializeField] public Sprite Sprite { get; set; }
}

public class Cell : UICompanent, IPointerDownHandler {
    public event Action<Cell> Selected;

    [Header("CellStateConfigs")]
    [SerializeField] private CellStateConfig _emptyConfig;
    [SerializeField] private CellStateConfig _activeConfig;

    [Space(10)]
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _textLabel;

    private CellStateConfig _fillConfig;
    private Vector2 _position;
    private string _textValue;

    public CellStates CurrentState { get; private set; } = CellStates.Empty;
    
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

    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log($"{Position}");
        Selected?.Invoke(this); 
    }

    public void SetState(CellStates state, Color color = default) {
        CurrentState = state;

        if (CurrentState == CellStates.Fill) 
            _fillConfig = new CellStateConfig(_emptyConfig.Sprite, color);
        
        FillingCompanents();
    }

    private void FillingCompanents() {
        var config = GetConfigByState(CurrentState);
        
        _background.color = config.Color;
        _background.sprite = config.Sprite;
    }

    private CellStateConfig GetConfigByState(CellStates state) {
        switch (state) {
            case CellStates.Empty:
                return _emptyConfig;
               
            case CellStates.Active:
                return _activeConfig;

            case CellStates.Fill:
                return _fillConfig;

            default:
                throw new ArgumentException($"Invalid CellStates: {state}");
        }
    }
}
