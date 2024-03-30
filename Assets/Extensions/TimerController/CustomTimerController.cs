using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.TimerController
{
    public class CustomTimerController : MonoBehaviour
    {
        private HashSet<ITimer> _allTimers = new HashSet<ITimer>();
        private Queue<ITimer> _timersToAdd = new Queue<ITimer>();
        private Queue<ITimer> _timersToRemove = new Queue<ITimer>();
        private void Update()
        {
            if (_allTimers != null && _allTimers.Count > 0)
            {
                foreach (ITimer customTimer in _allTimers)
                {
                    customTimer.UpdateTimer(Time.deltaTime);
                }
            }

            while (_timersToAdd.Count > 0)
            {
                var timerToAdd = _timersToAdd.Dequeue();
                _allTimers.Add(timerToAdd);
                timerToAdd.StartTimer();
            }

            while (_timersToRemove.Count > 0)
            {
                var timerToRemove = _timersToRemove.Dequeue();
                timerToRemove.StopTimer();
                _allTimers.Remove(timerToRemove);
            }
        }
        public void Register(ITimer customTimer)
        {
            _timersToAdd.Enqueue(customTimer);
        }
        public void Unregister(ITimer customTimer)
        {
            _timersToRemove.Enqueue(customTimer);
        }
    }
}
