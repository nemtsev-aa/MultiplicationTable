using UnityEngine;
using Zenject;

public class Bootstrapper : MonoBehaviour {
    [SerializeField] private UIManager _uIManager;
    //[SerializeField] private DialogFactory _dialogFactory;
    //[SerializeField] private UICompanentsFactory _companentsFactory;

    //[Inject]
    //private void Construct(UICompanentsFactory companentsFactory, DialogFactory dialogFactory) {
    //    _companentsFactory = companentsFactory;
    //    _dialogFactory = dialogFactory;
    //}

    private void Start() {
        _uIManager.Init();
    }
}
