using UnityEngine;

public class Bootstrapper : MonoBehaviour {
    [SerializeField] private UIManager _uIManager;

    private void Start() {
        Logger.Instance.Log($"Bootstrapper: Start");
        _uIManager.Init();
    }
}
