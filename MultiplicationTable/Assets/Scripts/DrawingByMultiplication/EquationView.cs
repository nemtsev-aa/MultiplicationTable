using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquationView : UICompanent {
    [SerializeField] private TextMeshProUGUI _multipliableText;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Image _backgroundColor;

    private EquationData _data;

    public int Multiplier => _data.Multiplier;

    public void Init(EquationViewConfig config) {
        _data = config.Data;

        FillingCompanents();
    }

    private void FillingCompanents() {
        _multipliableText.text = $"{_data.Multipliable}";
        _resultText.text = $"{_resultText}";
        _backgroundColor.color = _data.BaseColor;
    }
}
