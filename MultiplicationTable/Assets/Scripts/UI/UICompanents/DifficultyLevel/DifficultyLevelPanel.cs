using UnityEngine;

public class DifficultyLevelPanel : UIPanel {
    [SerializeField] private DifficultyLevelSelector _levelSelectorPrefab;
    [SerializeField] private RectTransform _selectorsParent;

    public void Init() {
        
    }

    //(EnemyType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length)
}
