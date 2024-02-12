using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EquationSlot : UICompanent, IDropHandler {
    public event Action ItemValueChanged;

    [SerializeField] private Image _backgroundImage;
    private float _duration = 0.05f;

    private EquationSlotConfig _config;
    private EquationItem _currentItem;

    public EquationItem CurrentItem {
        get {
            return _currentItem;
        }
        set {
            if (value == null) {
                _currentItem = null;
                BackgroundImageFadeAnimation(true);
                return;
            }

            if (value.Equals(_currentItem))
                return;

            _currentItem = value;
            BackgroundImageFadeAnimation(false);
        }
    }

    private void BackgroundImageFadeAnimation(bool status) {
        float fadeValue = (status == true) ? 0.5f : 0f;

        var s = DOTween.Sequence();
        var scaledRunTime = _duration;
        var fadedTime = _duration;

        Transform transform = _backgroundImage.transform;
        s.Append(transform.DOScale(transform.localScale.x * 0.5f, scaledRunTime));
        s.Append(_backgroundImage.DOFade(fadeValue, fadedTime));
        s.Append(transform.DOScale(Vector3.one, scaledRunTime));

        _backgroundImage.raycastTarget = status;
        //Debug.Log($"{gameObject.name}: CurrentItem: {_currentItem.gameObject.name}");
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
        
        if (CurrentItem != null) 
            CurrentItem.ParentAfterDrag = item.ParentAfterDrag;
        
        item.ParentAfterDrag = transform;
        item.Slot = this;

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
