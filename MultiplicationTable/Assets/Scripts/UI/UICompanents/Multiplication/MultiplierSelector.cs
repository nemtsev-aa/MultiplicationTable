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

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _statusIcon;

    [SerializeField] private Sprite _compliteSprite;
    [SerializeField] private Sprite _unCompliteSprite;

    private int _index;
    private MultiplierSelectorStatus _status;
    
    public void Int(MultiplierButtonConfig config) {
        _index = config.Index;
        _status = config.Status;

        FillingOutPartners();

        _button.onClick.AddListener(ButtonClick);
    }

    private void FillingOutPartners() {
        _label.text = $"{_index}X";

        var sprite = GetSpriteByType();
        if (sprite != null) {
            _statusIcon.gameObject.SetActive(true);
            _statusIcon.sprite = sprite;
        }
        else
            _statusIcon.gameObject.SetActive(false);
    }

    private Sprite GetSpriteByType() {
        switch (_status) {
            case MultiplierSelectorStatus.Complite:
                return _compliteSprite;

            case MultiplierSelectorStatus.UnConplite:
                return _unCompliteSprite;

            case MultiplierSelectorStatus.Empty:
                return null;

            default:
                throw new ArgumentException(nameof(_status));
        }
    }

    private void ButtonClick() => MultiplierSelected?.Invoke(_index);

}
