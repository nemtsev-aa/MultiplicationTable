using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private UICompanentsFactory _uiCompanentsFactory;
    [SerializeField] private DialogFactory _dialogFactory;

    public override void InstallBindings() {
        BindFactories();
    }

    private void BindFactories() {
        Container.Bind<UICompanentsFactory>().FromInstance(_uiCompanentsFactory);
        Container.Bind<DialogFactory>().FromInstance(_dialogFactory);
    }
}
