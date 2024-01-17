using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogFactory", menuName = "Factory/DialogFactory")]
public class DialogFactory : ScriptableObject {
    private const string PrefabsFilePath = "Dialogs/";
    private RectTransform _dialogsParent;

    private static readonly Dictionary<Type, string> _prefabsDictionary = new Dictionary<Type, string>() {
            {typeof(MainMenuDialog), nameof(MainMenuDialog)},
            {typeof(LearningModeDialog), nameof(LearningModeDialog)},
            {typeof(TrainingModeDialog), nameof(TrainingModeDialog)},
            {typeof(SettingsDialog), nameof(SettingsDialog)},
            {typeof(HistoryDialog), nameof(HistoryDialog)},
    };

    public void Init(RectTransform dialogsParent) => _dialogsParent = dialogsParent;

    public T GetDialog<T>() where T : Dialog {
        var go = GetPrefabByType<T>();

        if (go == null)
            return null;

        return (T)Instantiate<Dialog>(go, _dialogsParent);
    }

    private T GetPrefabByType<T>() where T : Dialog {
        var prefabName = _prefabsDictionary[typeof(T)];

        if (string.IsNullOrEmpty(prefabName)) {
            Debug.LogError("Cant find prefab type of " + typeof(T) + "Do you added it in PrefabsDictionary?");
        }

        var path = PrefabsFilePath + _prefabsDictionary[typeof(T)];
        var dialog = Resources.Load<T>(path);

        if (dialog == null)
            Debug.LogError("Cant find prefab at path " + path);

        return dialog;
    }
}