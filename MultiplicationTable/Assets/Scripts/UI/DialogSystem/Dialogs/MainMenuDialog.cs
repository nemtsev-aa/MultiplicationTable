using System;
using UnityEngine;

public class MainMenuDialog : Dialog {
    public event Action<ModeTypes> ApplicationModeSelected;

    private ModePanel _modePanel;
    private ModeTypes _currentModeType;

    public override void Init() {
        base.Init();
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _modePanel = GetPanelByType<ModePanel>();
        _modePanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _modePanel.ModeSelected += OnModeSelected;
    }


    public override void RemoveListeners() {
        base.RemoveListeners();

        _modePanel.ModeSelected -= OnModeSelected;
    }

    private void OnModeSelected(ModeTypes type) {
        _currentModeType = type;

        Debug.Log($"Selection ModeType: {_currentModeType}");
        ApplicationModeSelected?.Invoke(type);
    }
}
