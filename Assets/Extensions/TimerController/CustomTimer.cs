using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace MyExtensions.TimerController
{
    public interface ITimer
    {
        public bool UpdateTimer(float deltaTime);
        public void StartTimer();
        public void StopTimer();
        public void SetDuration(float duration);
        public Action TimerCompleted { get; set; }
    }

    [Serializable]
    public class CustomTimer : ITimer
    {
        [SerializeField] private float _duration;
        [SerializeField] private bool _isLoop;
        [SerializeField] private float _remainingTime;
        private bool _isActive;
        public bool IsActive { get => _isActive; private set => _isActive = value; }
        public Action TimerCompleted { get; set; }

        public bool UpdateTimer(float deltaTime)
        {
            if (_isActive)
            {
                _remainingTime -= deltaTime;

                if (_remainingTime <= 0)
                {
                    _isActive = _isLoop;
                    _remainingTime = _duration;
                    TimerCompleted?.Invoke();
                    return true;
                }
            }
            return false;
        }
        public void StartTimer()
        {
            _remainingTime = _duration;
            _isActive = true;
        }

        public void SetDuration(float duration)
        {
            _duration = Mathf.Max(duration, 0f);
            _remainingTime = _duration;
        }
        public void StopTimer()
        {
            _isActive = false;
        }
    }
}
