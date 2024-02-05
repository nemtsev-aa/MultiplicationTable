public class TimerBar : Bar {

    private Timer _timer;

    public void Init(Timer timer) => _timer = timer;

    private void OnEnable() {
        if (_timer != null)
            _timer.HasBeenUpdated += OnValueChanged;
    }

    private void OnDisable() {
        if (_timer != null)
            _timer.HasBeenUpdated -= OnValueChanged;
    }

    protected override void OnValueChanged(float valueInParts) {
        Filler.fillAmount = valueInParts;
    }
}
