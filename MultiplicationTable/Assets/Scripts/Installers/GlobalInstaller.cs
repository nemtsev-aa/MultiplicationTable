using System;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller {
    [SerializeField] private UICompanentPrefabs _uiCompanentPrefabs;
    [SerializeField] private ModsConfig _modsConfig;
    [SerializeField] private LevelsConfig _levelsConfig;
    [SerializeField] private QuestionsConfig _questionsConfig;

    public override void InstallBindings() {
        BuildModsConfig();
        BuildQuestionsConfig();
        BuildLevelsConfig();
        BindUICompanentsConfig();
        BindFactories();
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

    private void BuildLevelsConfig() {
        if (_levelsConfig.Configs.Count == 0)
            Debug.LogError($"List of LevelsConfig is empty");

        Container.Bind<LevelsConfig>().FromInstance(_levelsConfig).AsSingle();
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
}
