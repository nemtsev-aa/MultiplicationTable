using System;
using UnityEngine;

public class MultiplicationView : MonoBehaviour {
    [SerializeField] private MultiplierButton _multiplierViewPrefab;
    [SerializeField] private int _startValue;
    [SerializeField, Range(1,8)] private int _buttonCount;

    private void Start() {
        CreateButtons();
    }

    private void CreateButtons() {
        for (int i = 0; i < _buttonCount; i++) {
            MultiplierButton newButton = Instantiate(_multiplierViewPrefab);

            newButton.Int(_startValue, MultiplierButtonStatus.Complite);
            newButton.transform.SetParent(transform);
            newButton.MultiplierSelected += OnMultiplierSelected;

            _startValue++;
        }
    }

    private void OnMultiplierSelected(int level) {
        Debug.Log($"{level}");
    }
}
