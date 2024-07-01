using UnityEngine;
using System;
using System.Collections;

namespace PUZ.Timer
{
    public class FleeTimer : MonoBehaviour
    {
        private float _timer;

        private IEnumerator _timerCoroutine;

        public Action OnTimeOut;

        private void TimeOut()
        {
            Debug.Log("Your timer is up.");
        }

        public void StartTimer(float timer, Action onTimeOut)
        {
            OnTimeOut = onTimeOut;
            _timer = 0f;
            _timerCoroutine = StartTimer(timer);
            StartCoroutine(_timerCoroutine);
        }

        private IEnumerator StartTimer(float totalTime)
        {
            while (_timer < totalTime)
            {
                yield return new WaitForSecondsRealtime(1);
                _timer++;
                Debug.Log("Timer is : " + _timer);
            }

            OnTimeOut?.Invoke();
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
                OnTimeOut = null;
            }
        }
    }
}

