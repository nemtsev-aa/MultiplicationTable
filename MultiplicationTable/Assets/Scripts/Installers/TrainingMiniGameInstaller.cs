using UnityEngine;
using Zenject;

public class TrainingMiniGameInstaller : MonoInstaller {
    [SerializeField] private LineSpawner _lineSpawner;
    [SerializeField] private Line _linetPrefab;

    public override void InstallBindings() {
        BindLinePrefab();
        BildFactories();
        BindLineSpawner();
    }

    private void BindLineSpawner() {
        Container.Bind<LineSpawner>().FromInstance(_lineSpawner).AsSingle();      
    }

    private void BindLinePrefab() {
        if (_linetPrefab = null)
            Debug.LogError($"LinePrefabs is empty");

        Container.Bind<Line>().FromInstance(_linetPrefab).AsSingle();
    }

    private void BildFactories() {
        Container.Bind<LineFactory>().AsSingle();
    }

}
