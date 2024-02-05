using System;
using System.Collections;
using UnityEngine;

public class Timer {
    public event Action TimeIsOver;
    public event Action<float> HasBeenUpdated;
    
    private float _time;
    private float _remainingTime;
    private float _elapsedTime;

    private MonoBehaviour _context;
    private IEnumerator _countdown;
    private IEnumerator _countup;

    public Timer(MonoBehaviour context) => _context = context;

    public void Set(float time) {
        _time = time;
        _remainingTime = time;
        _elapsedTime = 0f;
    }

    public void StartCountingTime() {
        _countdown = Countdown();
        _context.StartCoroutine(_countdown);
    }

    public void StopCountingTime() {
        if (_countdown != null)
            _context.StopCoroutine(_countdown);
    }

    public void StartStopwatch() {
        _countup = Countup();
        _context.StartCoroutine(_countup);
    }

    public void StopStopwatch() {
        if (_countup != null)
            _context.StopCoroutine(_countup);
    }

    private IEnumerator Countdown() {
        while (_remainingTime >= 0) {
            _remainingTime -= Time.deltaTime;

            HasBeenUpdated?.Invoke(_remainingTime / _time);
            yield return null;
        }
    }

    private IEnumerator Countup() {
        while (_elapsedTime <= _time) {
            _elapsedTime += Time.deltaTime;

            HasBeenUpdated?.Invoke(_elapsedTime / _time);
            yield return null;
        }
    }
}
