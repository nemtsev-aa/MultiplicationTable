using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquationSlot : UICompanent, IDropHandler {
    public event Action ItemValueChanged;

    [SerializeField] private Image _backgroundImage;

    private EquationSlotConfig _config;
    private EquationItem _currentItem;

    public EquationItem CurrentItem {
        get {
            return _currentItem;
        }
        set {
            if (value == null) {
                _currentItem = null;
                _backgroundImage.gameObject.SetActive(true);
                Debug.Log($"{gameObject.name}: CurrentItem: NULL" );
                return;
            }

            if (value.Equals(_currentItem))
                return;

            _currentItem = value;
            _backgroundImage.gameObject.SetActive(false);
            Debug.Log($"{gameObject.name}: CurrentItem: {_currentItem.gameObject.name}");
        }
    }

    public void Init(EquationSlotConfig config) {
        _config = config;

        SetItem(_config.Item);
    }

    public void OnDrop(PointerEventData eventData) {
        var otherNumberTransform = eventData.pointerDrag.transform;

        if (otherNumberTransform.TryGetComponent(out EquationItem item)) 
            SetItem(item);
        
    }

    public void SetItem(EquationItem item) {
        if (item == null)
            return;

        if (item.ParentAfterDrag == transform)
            return;
        
        if (CurrentItem != null) {
            CurrentItem.ParentAfterDrag = item.ParentAfterDrag;
        }

        item.ParentAfterDrag = transform;
        item.Slot = this;

        //CurrentItem = item;

        ItemValueChanged?.Invoke();
    }

    private void OnParentChanged(EquationSlot slot) {
        slot.SetItem(CurrentItem);
        CurrentItem.ParentChanged -= OnParentChanged;
        
        CurrentItem = null;

        _backgroundImage.gameObject.SetActive(true);
        ItemValueChanged?.Invoke();
    }
}
