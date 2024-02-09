using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EquationItemsPanel : UIPanel {
    //public event Action<int> CountSolvedEquationsChanged;

    [SerializeField] private RectTransform _itemsParent;
    [SerializeField] private RectTransform _slotsParent;

    private UICompanentsFactory _factory;
    private GridLayoutGroup _grid;

    private List<EquationSlot> _equationSlots;
    private List<EquationItem> _equationItems;

    private List<EquationData> _equations;
    private int _emptyEquationSlotCount;

    private bool _hideAfterSelection;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init(List<EquationData> equations, bool hideAfterSelection = true) {
        _equations = equations;
        _hideAfterSelection = hideAfterSelection;

        _grid = _slotsParent.GetComponent<GridLayoutGroup>();
        _grid.enabled = true;

        CreateEquationItems();
        CreateEquationSlots();

        Invoke(nameof(SetGridLayoutGroup), 1f);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var item in _equationItems) {
            item.ParentCleared -= OnParentCleared;
        }
    }

    public override void Reset() {
        base.Reset();

        foreach (var iSlot in _equationSlots) {
            Destroy(iSlot.gameObject);
        }
        _equationSlots.Clear();

        foreach (var iItem in _equationItems) {
            Destroy(iItem.gameObject);
        }
        _equationItems.Clear();
    }

    private void CreateEquationItems() {
        Canvas mainCanvas = GetComponentInParent<Canvas>();
        List<EquationItem> equationItems = new List<EquationItem>();

        foreach (var iEquation in _equations) {
            equationItems.Add(GetEquationItem(iEquation.Multipliable, mainCanvas));
            equationItems.Add(GetEquationItem(iEquation.Multiplier, mainCanvas));
        }

        _equationItems = Shuffle(equationItems);
    }

    private EquationItem GetEquationItem(int value, Canvas mainCanvas) {
        EquationItemConfig config = new EquationItemConfig(value, mainCanvas, _slotsParent);

        EquationItem newItem = _factory.Get<EquationItem>(config, _itemsParent);
        newItem.Init(config);
        newItem.ParentCleared += OnParentCleared;

        return newItem;
    }

    private void SetGridLayoutGroup() {
        _grid.enabled = false;
    }

    private List<EquationItem> Shuffle(List<EquationItem> list) {
        EquationItem[] array = list.ToArray();

        for (int i = array.Length - 1; i > 0; i--) {
            int rnd = Random.Range(0, i);
            EquationItem temp = array[i];

            array[i] = array[rnd];
            array[rnd] = temp;
        }

        return array.ToList();
    }

    private void CreateEquationSlots() {
        _equationSlots = new List<EquationSlot>();

        foreach (var item in _equationItems) {
            EquationSlotConfig config = new EquationSlotConfig(item);
            EquationSlot newSlot = _factory.Get<EquationSlot>(config, _slotsParent);
            newSlot.Init(config);

            _equationSlots.Add(newSlot);
        }
    }

    private void OnParentCleared(EquationItem item) {
        item.SetSlot(GetEmptySlot());
    }

    private EquationSlot GetEmptySlot() {
        return _equationSlots.FirstOrDefault(slot => slot.CurrentItem == null);
    }
}
