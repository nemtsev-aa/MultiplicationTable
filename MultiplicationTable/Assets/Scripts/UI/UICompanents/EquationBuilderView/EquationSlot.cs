using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquationSlot : UICompanent, IDropHandler {
    public event Action ItemValueChanged;

    [SerializeField] private Image _backgroundImage;

    private EquationSlotConfig _config;

    public EquationItem CurrentItem { get; private set; }


    public void Init(EquationSlotConfig config) {
        _config = config;

        SetItem(_config.Item);
    }

    public void OnDrop(PointerEventData eventData) {
        var otherNumberTransform = eventData.pointerDrag.transform;

        if (otherNumberTransform.TryGetComponent(out EquationItem item)) {
            if (CurrentItem != null && CurrentItem.Equals(item))
                return;

            SetItem(item);
        }
    }

    public void SetItem(EquationItem item) {
        if (item == null)
            return;

        item.SetSlot(this);
        item.ParentChanged += OnParentChanged;

        CurrentItem = item;
        ItemValueChanged?.Invoke();

        _backgroundImage.gameObject.SetActive(false);
    }

    private void OnParentChanged(EquationSlot slot) {
        slot.SetItem(CurrentItem);
        CurrentItem.ParentChanged -= OnParentChanged;
        
        CurrentItem = null;

        _backgroundImage.gameObject.SetActive(true);
        ItemValueChanged?.Invoke();
    }
}
