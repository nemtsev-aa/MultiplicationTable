using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;
   
    public override void InstallBindings() {
        BindUICompanentsConfig();
        BindFactories();
    }

    private void BindUICompanentsConfig() {
        Container.Bind<UICompanentPrefabs>().FromInstance(_uiCompanentPrefabs).AsSingle();
    }

    private void BindFactories() {
        Container.Bind<DialogFactory>().AsSingle();
        Container.Bind<UICompanentsFactory>().AsSingle();
    }
}
