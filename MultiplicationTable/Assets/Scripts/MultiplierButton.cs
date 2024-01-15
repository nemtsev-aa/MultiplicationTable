using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum MultiplierButtonStatus {
    Complite = 1,
    UnConplite = 2,
    Empty = 3
}

public class MultiplierButton : MonoBehaviour {
    public event Action<int> MultiplierSelected;

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Image _statusIcon;

    [SerializeField] private Sprite _compliteSprite;
    [SerializeField] private Sprite _unCompliteSprite;

    private int _index;
    private MultiplierButtonStatus _status;
    public void Int(int index, MultiplierButtonStatus status) {
        _index = index;
        _status = status;

        FillingOutPartners();

        _button.onClick.AddListener(ButtonClick);
    }

    private void FillingOutPartners() {
        _label.text = $"{_index}X";

        switch (_status) {
            case MultiplierButtonStatus.Complite:
                _statusIcon.sprite = _compliteSprite;

                break;

            case MultiplierButtonStatus.UnConplite:

                _statusIcon.sprite = _unCompliteSprite;
                break;

            case MultiplierButtonStatus.Empty:

                _statusIcon.gameObject.SetActive(false);
                break;

            default:
                break;
        }  
    }

    private void ButtonClick() => MultiplierSelected?.Invoke(_index);

}
