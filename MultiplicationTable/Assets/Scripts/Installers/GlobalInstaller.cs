using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;
    [SerializeField] private ModsConfig _modsConfig;
    [SerializeField] private DifficultyLevelsConfig _difficultyLevelsConfig;
    [SerializeField] private QuestionsConfig _questionsConfig;
    [SerializeField] private DrawingsConfig _drawingsConfig;
    [SerializeField] private TrainingGameConfigs _trainingGameConfigs;
    [SerializeField] private Transform _linesParent;
    [SerializeField] private Pointer _pointerPrefab;

    public override void InstallBindings() {
        BuildModsConfig();
        BuildQuestionsConfig();
        BuildDrawingsConfig();
        BuildDifficultyLevelsConfig();
        BuildTrainingGameConfigs();
        BindUICompanentsConfig();

        BildPointer();
        BindLineSpawner();

        BindFactories();
        BindTimeCounter();
        BindInput();
    }

    private void BuildModsConfig() {
        if (_modsConfig.Configs.Count == 0)
            Debug.LogError($"List of ModsConfig is empty");

        Container.Bind<ModsConfig>().FromInstance(_modsConfig).AsSingle();
    }

    private void BuildQuestionsConfig() {
        if (_questionsConfig.Equations.Count == 0)
            Debug.LogError($"List of QuestionsConfig is empty");

        Container.Bind<QuestionsConfig>().FromInstance(_questionsConfig).AsSingle();
    }

    private void BuildDrawingsConfig() {
        if (_drawingsConfig.Drawings.Count == 0)
            Debug.LogError($"List of DrawingsConfig is empty");

        Container.Bind<DrawingsConfig>().FromInstance(_drawingsConfig).AsSingle();
    }

    private void BuildDifficultyLevelsConfig() {
        if (_difficultyLevelsConfig.Configs.Count == 0)
            Debug.LogError($"List of DifficultyLevelsConfig is empty");

        Container.Bind<DifficultyLevelsConfig>().FromInstance(_difficultyLevelsConfig).AsSingle();
        Container.BindInstance(new EquationFactory(_difficultyLevelsConfig));
    }

    private void BuildTrainingGameConfigs() {
        if (_trainingGameConfigs.Configs.Count == 0)
            Debug.LogError($"List of TrainingGameConfigs is empty");

        Container.Bind<TrainingGameConfigs>().FromInstance(_trainingGameConfigs).AsSingle();
    }

    private void BindUICompanentsConfig() {
        if (_uiCompanentPrefabs.Prefabs.Count == 0)
            Debug.LogError($"List of UICompanentPrefabs is empty");

        Container.Bind<UICompanentPrefabs>().FromInstance(_uiCompanentPrefabs).AsSingle();
    }

    private void BindFactories() {
        Container.Bind<DialogFactory>().AsSingle();
        Container.Bind<UICompanentsFactory>().AsSingle();
    }

    private void BindTimeCounter() {
        TimeCounter timeCounter = new TimeCounter();

        Container.BindInstance(timeCounter).AsSingle();
        Container.BindInterfacesAndSelfTo<ITickable>().FromInstance(timeCounter).AsSingle();
    }

    private void BildPointer() {
        Pointer pointer = Container.InstantiatePrefabForComponent<Pointer>(_pointerPrefab);
        Container.Bind<Pointer>().FromInstance(pointer).AsSingle();
    }

    private void BindLineSpawner() {
        LineFactory factory = new LineFactory(Container);
        Container.Bind<LineFactory>().FromInstance(factory).AsSingle();

        Container.Bind<LineSpawner>().AsSingle().NonLazy(); 
    }

    private void BindInput() {
        if (SystemInfo.deviceType == DeviceType.Handheld)
            Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
        else
            Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();

        Container.Bind<MovementHandler>().AsSingle().NonLazy();
    }
}