using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour, IDisposable {
    public event Action BackClicked;
    public event Action SettingsClicked;
    public event Action ShareClicked;
    public event Action HistoryClicked;

    [SerializeField] protected Button BackButton;
    [SerializeField] protected Button SettingsButton;
    [SerializeField] protected Button ShareButton;
    [SerializeField] protected Button HistoryButton;

    [SerializeField] protected List<UIPanel> Panels = new List<UIPanel>();

    protected DialogMediator Mediator;

    public bool IsInit { get; protected set; } = false;
    public IReadOnlyList<UIPanel> DialogPanels => Panels;
    
    public virtual void Init() {
        if (IsInit == true)
            return;

        InitializationPanels();
        AddListeners();

        IsInit = true;
    }

    public virtual void Show(bool value) {
        gameObject.SetActive(value);
    }

    public virtual void ShowPanel<T>(bool value) where T : UIPanel {
        T panel = (T)Panels.First(panel => panel is T);

        panel.UpdateContent();
        panel.Show(value);
    }

    public virtual void ResetPanels() {
        foreach (var iPanel in Panels) {
            iPanel.Reset();
        }
    }

    public virtual void AddListeners() {
        if (BackButton != null)
            BackButton.onClick.AddListener(BackButtonClick);
        
        if (SettingsButton != null)
            SettingsButton.onClick.AddListener(SettingsButtonClick);

        if (ShareButton != null)
            ShareButton.onClick.AddListener(ShareButtonClick);

        if (HistoryButton != null)
            HistoryButton.onClick.AddListener(HistoryButtonClick);
    }

    public virtual void RemoveListeners() {
        BackButton.onClick.RemoveListener(BackButtonClick);
        SettingsButton.onClick.RemoveListener(SettingsButtonClick);
        ShareButton.onClick.RemoveListener(ShareButtonClick);
        HistoryButton.onClick.RemoveListener(HistoryButtonClick);
    }

    public virtual T GetPanelByType<T>() where T : UIPanel {
        return (T)Panels.FirstOrDefault(panel => panel is T);
    }

    public abstract void InitializationPanels();

    public abstract void PreparingForClosure();
    
    private void BackButtonClick() {
        PreparingForClosure();
        ResetPanels();
        BackClicked?.Invoke();
    }

    private void SettingsButtonClick() => SettingsClicked?.Invoke();

    private void ShareButtonClick() => ShareClicked?.Invoke();

    private void HistoryButtonClick() => HistoryClicked?.Invoke();

    public void Dispose() {
        RemoveListeners();
    }
}