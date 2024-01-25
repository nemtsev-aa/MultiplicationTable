using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IDisposable {
    [SerializeField] private RectTransform _dialogsParent;

    private UICompanentsFactory _companentsFactory;
    private DialogFactory _dialogFactory;
    private DialogSwitcher _dialogSwitcher;
    private DialogMediator _dialogMediator;

    private Dictionary<DialogTypes, Dialog> _dialogsDictionary;
    private List<Dialog> _dialogs;
    
    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, DialogFactory dialogFactory) {
        _companentsFactory = companentsFactory;
        _dialogFactory = dialogFactory;
    }

    public void Init() {
        _dialogFactory.SetDialogsParent(_dialogsParent);
        
        CreateDialogs();

        _dialogSwitcher = new DialogSwitcher(this);
        _dialogMediator = new DialogMediator(this, _dialogSwitcher);
        _dialogMediator.Init();

        _dialogSwitcher.ShowDialog(DialogTypes.MainMenu);
    }

    public Dialog GetDialogByType(DialogTypes type) {
        if (_dialogsDictionary.Keys.Count == 0)
            throw new ArgumentNullException("DialogsDictionary is empty");

        return _dialogsDictionary[type];
    }

    public List<Dialog> GetDialogList() {
        return _dialogsDictionary.Values.ToList();
    }

    private void CreateDialogs() {
        _dialogsDictionary = new Dictionary<DialogTypes, Dialog> {
                { DialogTypes.MainMenu, _dialogFactory.GetDialog<MainMenuDialog>()},
                { DialogTypes.LearningMode, _dialogFactory.GetDialog<LearningModeDialog>()},
                { DialogTypes.TrainingMode, _dialogFactory.GetDialog<TrainingModeDialog>()},
                { DialogTypes.ExamMode, _dialogFactory.GetDialog<ExamModeDialog>()},
                { DialogTypes.Settings, _dialogFactory.GetDialog<SettingsDialog>()},
                { DialogTypes.History, _dialogFactory.GetDialog<HistoryDialog>()},
                { DialogTypes.DrawingByMultiplication, _dialogFactory.GetDialog<DrawingByMultiplicationDialog>()}
            };

        foreach (var iDialog in _dialogsDictionary.Values) {
            iDialog.Init();
            iDialog.Show(false);
        }
    }

    public void Dispose() {
        
    }
}
