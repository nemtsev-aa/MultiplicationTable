using UnityEngine;
using System;
using System.Collections.Generic;

public class DialogMediator : IDisposable {
    private UIManager _uIManager;

    private MainMenuDialog _mainMenuDialog;
    private LearningModeDialog _learningModeDialog;
    private TrainingModeDialog _trainingModeDialog;
    private ExamModeDialog _examModeDialog;
    private SettingsDialog _settingsDialog;
    private HistoryDialog _historyDialog;

    private DrawingByMultiplicationDialog _drawingDialog;

    private DialogSwitcher _dialogSwitcher;
    private List<Dialog> _dialogs;

    public DialogMediator(UIManager uIManager, DialogSwitcher dialogSwitcher) {
        _uIManager = uIManager;
        _dialogSwitcher = dialogSwitcher;
    }

    public void Init() {
        GetDialogs();
        AddListeners();
    }

    private void GetDialogs() {
        _mainMenuDialog = _uIManager.GetDialogByType(DialogTypes.MainMenu).GetComponent<MainMenuDialog>();
        _learningModeDialog = _uIManager.GetDialogByType(DialogTypes.LearningMode).GetComponent<LearningModeDialog>();
        _trainingModeDialog = _uIManager.GetDialogByType(DialogTypes.TrainingMode).GetComponent<TrainingModeDialog>();
        _examModeDialog = _uIManager.GetDialogByType(DialogTypes.ExamMode).GetComponent<ExamModeDialog>();
        _settingsDialog = _uIManager.GetDialogByType(DialogTypes.Settings).GetComponent<SettingsDialog>();
        _historyDialog = _uIManager.GetDialogByType(DialogTypes.History).GetComponent<HistoryDialog>();

        _drawingDialog = _uIManager.GetDialogByType(DialogTypes.DrawingByMultiplication).GetComponent<DrawingByMultiplicationDialog>();

        _dialogs = new List<Dialog>() {
            _mainMenuDialog,
            _learningModeDialog,
            _trainingModeDialog,
            _examModeDialog,
            _settingsDialog,
            _historyDialog,
            _drawingDialog
        };
    }

    private void AddListeners() {
        foreach (var iDialog in _dialogs) {
            Debug.Log($"{iDialog.name}");
            iDialog.SettingsClicked += OnSettingsClicked;
            iDialog.ShareClicked += OnShareClicked;
            iDialog.HistoryClicked += OnHistoryClicked;
            iDialog.BackClicked += OnBackClicked;
        }

        SubscribeToMainMenuDialogActions();
        SubscribeToTrainingDialogActions();
    }

    private void RemoveListeners() {
        foreach (var iDialog in _dialogs) {
            iDialog.BackClicked -= OnBackClicked;
            iDialog.SettingsClicked -= OnSettingsClicked;
            iDialog.ShareClicked -= OnShareClicked;
            iDialog.HistoryClicked -= OnHistoryClicked;
        }

        UnsubscribeToMainMenuDialogActions();
        UnsubscribeToTrainingDialogActions();
    }

    private void OnBackClicked() => _dialogSwitcher.ShowDialog(DialogTypes.MainMenu);

    private void OnSettingsClicked() => _dialogSwitcher.ShowDialog(DialogTypes.Settings);

    private void OnShareClicked() => Debug.Log($"Share Button Clicked!");

    private void OnHistoryClicked() => Debug.Log($"History Button Clicked!");

    #region MainMenuDialogActions

    private void SubscribeToMainMenuDialogActions() {
        _mainMenuDialog.ApplicationModeSelected += OnApplicationModeSelected;
    }

    private void UnsubscribeToMainMenuDialogActions() {
        _mainMenuDialog.ApplicationModeSelected -= OnApplicationModeSelected;
    }

    private void OnApplicationModeSelected(ModeTypes type) {
        switch (type) {
            case ModeTypes.Learning:
                _dialogSwitcher.ShowDialog(DialogTypes.LearningMode);
                break;

            case ModeTypes.Training:
                _dialogSwitcher.ShowDialog(DialogTypes.TrainingMode);
                break;

            case ModeTypes.Exam:
                _dialogSwitcher.ShowDialog(DialogTypes.ExamMode);
                break;

            default:
                throw new ArgumentException($"Invalid ModeTypes: {type}");
        }
    }

    #endregion

    #region TrainingModeActions
    private void SubscribeToTrainingDialogActions() {
        _trainingModeDialog.TrainingGameStarted += OnTrainingGameStarted;
    }

    private void UnsubscribeToTrainingDialogActions() {
        _trainingModeDialog.TrainingGameStarted -= OnTrainingGameStarted;
    }

    private void OnTrainingGameStarted(TrainingGameData data) {
        _trainingModeDialog.Show(false);

        switch (data.GameType) {
            case TrainingGameTypes.TimePressure:

                break;

            case TrainingGameTypes.Survival:

                break;

            case TrainingGameTypes.Drawing:
                _drawingDialog.SetTrainingGameData(data);
                _drawingDialog.Show(true);

                break;

            case TrainingGameTypes.Puzzles:

                break;

            case TrainingGameTypes.Accordance:

                break;

            default:

                break;
        }
    }
    #endregion

    public void Dispose() {
        RemoveListeners();
    }
}