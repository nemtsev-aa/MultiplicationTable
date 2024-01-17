using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Dialog : MonoBehaviour, IDisposable {
    public event Action BackClicked;

    [SerializeField] protected Button BackButton;
    [SerializeField] protected List<UIPanel> Panels = new List<UIPanel>();

    protected DialogMediator Mediator;

    public bool IsInit { get; protected set; } = false;
    
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

    public virtual void InitializationPanels() { }

    public virtual void AddListeners() {
        if (BackButton != null)
            BackButton.onClick.AddListener(BackButtonClick);
    }

    public virtual void RemoveListeners() {
        BackButton.onClick.RemoveListener(BackButtonClick);
    }

    public virtual T GetPanelByType<T>() where T : UIPanel {
        return (T)Panels.FirstOrDefault(panel => panel is T);
    }

    private void BackButtonClick() => BackClicked?.Invoke();
   
    public void Dispose() {
        RemoveListeners();
    }
}