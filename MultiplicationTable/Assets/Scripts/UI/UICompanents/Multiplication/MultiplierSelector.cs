using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MultiplierSelectorStatus {
    Complite = 1,
    UnConplite = 2,
    Empty = 3
}

public class MultiplierSelector : UICompanent {
    public event Action<int> MultiplierSelected;

    [SerializeField] private Toggle _toggle;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _statusIcon;
    
    [Space(10), Header("StatusSprites")]
    [SerializeField] private Sprite _compliteSprite;
    [SerializeField] private Sprite _unCompliteSprite;

    private Color _text—olorSelectedToggle;
    private Color _text—olorDeselectedToggle;

    private int _index;
    private MultiplierSelectorStatus _status;
    
    public void Int(MultiplierButtonConfig config, ToggleGroup group) {
        _index = config.Index;
        _status = config.Status;
        _toggle.group = group;

        SetColors();
        FillingInComponents();
    }

    private void OnEnable() {
        _toggle.onValueChanged.AddListener(ToggleClick);
    }

    private void OnDisable() {
        _toggle.onValueChanged.RemoveListener(ToggleClick);
    }

    private void SetColors() {
        _text—olorSelectedToggle = _toggle.colors.normalColor;
        _text—olorDeselectedToggle = _toggle.colors.selectedColor;
    }

    private void FillingInComponents() {
        _label.text = $"{_index}X";
        _label.color = _text—olorDeselectedToggle;

        SetSpriteByType();
    }

    private void SetSpriteByType() {
        switch (_status) {
            case MultiplierSelectorStatus.Complite:
                _statusIcon.gameObject.SetActive(true);
                _statusIcon.sprite = _compliteSprite;
                break;

            case MultiplierSelectorStatus.UnConplite:
                _statusIcon.gameObject.SetActive(true);
                _statusIcon.sprite = _unCompliteSprite;
                break;

            case MultiplierSelectorStatus.Empty:
                _statusIcon.gameObject.SetActive(false);
                break;
        }
    }

    private void ToggleClick(bool status) {
        if (status) {
            _label.color = _text—olorSelectedToggle;
            MultiplierSelected?.Invoke(_index);
        }
        else
            _label.color = _text—olorDeselectedToggle;
    }
}
