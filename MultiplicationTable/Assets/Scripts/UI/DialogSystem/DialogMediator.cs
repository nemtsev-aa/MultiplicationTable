using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class DialogMediator : IDisposable {
    private UIManager _uIManager;

    private MainMenuDialog _mainMenuDialog;
    private LearningModeDialog _learningModeDialog;
    private TrainingModeDialog _trainingModeDialog;
    private ExamModeDialog _examModeDialog;
    private SettingsDialog _settingsDialog;
    private HistoryDialog _historyDialog;

    private DrawingByMultiplicationDialog _drawingDialog;
    private TimePressureDialog _timePressureDialog;
    private SurvivalDialog _survivalDialog;
    private PuzzleDialog _puzzleDialog;
    private AccordanceDialog _accordanceDialog;

    private DialogSwitcher _dialogSwitcher;
    private List<Dialog> _dialogs;

    private List<TrainingGameDialog> _trainingGameDialogs;
    private TrainingGameDialog _trainingGameDialog;

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
        _timePressureDialog = _uIManager.GetDialogByType(DialogTypes.TimePressure).GetComponent<TimePressureDialog>();
        _survivalDialog = _uIManager.GetDialogByType(DialogTypes.Survival).GetComponent<SurvivalDialog>();
        _puzzleDialog = _uIManager.GetDialogByType(DialogTypes.Puzzle).GetComponent<PuzzleDialog>();
        _accordanceDialog = _uIManager.GetDialogByType(DialogTypes.Accordance).GetComponent<AccordanceDialog>();

        _dialogs = new List<Dialog>() {
            _mainMenuDialog,
            _learningModeDialog,
            _trainingModeDialog,
            _examModeDialog,
            _settingsDialog,
            _historyDialog,
            _drawingDialog,
            _timePressureDialog,
            _survivalDialog,
            _puzzleDialog,
            _accordanceDialog
        };

        _trainingGameDialogs = new List<TrainingGameDialog>() {
            _drawingDialog,
            _timePressureDialog,
            _survivalDialog,
            _puzzleDialog,
            _accordanceDialog
        };
    }

    private TrainingGameDialog GetTrainingGameDialog(TrainingGameTypes type) {
        return _trainingGameDialogs.FirstOrDefault(dialog => dialog.TrainingGameType == type);
    }

    private void AddListeners() {
        foreach (var iDialog in _dialogs) {
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

    private void OnBackClicked() => _dialogSwitcher.ShowPreviousDialog();

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
        _trainingGameDialog = GetTrainingGameDialog(data.GameType);
        _trainingGameDialog.TrainingGameFinished += OnTrainingGameFinished;

        _trainingGameDialog.SetTrainingGameData(data);
        _dialogSwitcher.ShowDialog(_trainingGameDialog.DialogType);
    }

    private void OnTrainingGameFinished(AttemptData data) {
        /////////////////////////////////////////////////
        // «аписать данные о попытке в историю попыток //
        /////////////////////////////////////////////////

        _trainingGameDialog.TrainingGameFinished -= OnTrainingGameFinished;
        _dialogSwitcher.ShowDialog(DialogTypes.TrainingMode);
    }

    #endregion

    public void Dispose() {
        RemoveListeners();
    }
}