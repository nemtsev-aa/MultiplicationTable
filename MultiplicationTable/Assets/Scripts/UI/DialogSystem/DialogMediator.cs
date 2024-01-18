using UnityEngine;
using System;

public class DialogMediator : IDisposable {
    private UIManager _uIManager;

    public DialogMediator(UIManager uIManager, DialogSwitcher dialogSwitcher) {
        _uIManager = uIManager;
        _dialogSwitcher = dialogSwitcher;

        GetDialogs();
        AddListeners();
    }

    private MainMenuDialog _mainMenuDialog;
    private LearningModeDialog _learningModeDialog;
    private TrainingModeDialog _trainingModeDialog;
    private SettingsDialog _settingsDialog;
    private HistoryDialog _historyDialog;

    private DialogSwitcher _dialogSwitcher;

    private void GetDialogs() {
        _mainMenuDialog = _uIManager.GetDialogByType(DialogTypes.MainMenu).GetComponent<MainMenuDialog>();
        _learningModeDialog = _uIManager.GetDialogByType(DialogTypes.LearningMode).GetComponent<LearningModeDialog>();
        _trainingModeDialog = _uIManager.GetDialogByType(DialogTypes.TrainingMode).GetComponent<TrainingModeDialog>();
        _settingsDialog = _uIManager.GetDialogByType(DialogTypes.Settings).GetComponent<SettingsDialog>();
        _historyDialog = _uIManager.GetDialogByType(DialogTypes.History).GetComponent<HistoryDialog>();
    }

    private void AddListeners() {
        SubscribeToMainMenuDialogActions();
    }

    private void RemoveListeners() {
        UnsubscribeToMainMenuDialogActions();
    }

    #region MainMenuDialogActions
    private void SubscribeToMainMenuDialogActions() {
        
    }

    private void UnsubscribeToMainMenuDialogActions() {
        
    }

    #endregion

    public void Dispose() {
        RemoveListeners();
    }
}