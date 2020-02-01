using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class RandomTimer : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTimerExpired;
        [SerializeField] private Vector2 timerDurationRange;
        [SerializeField] private bool loop;
        private float currentDuration;

        [SerializeField] private Animation testAnimation;

        private void Start()
        {
            StartTimer();
        }

        private void Update()
        {
            if(currentDuration > 0)
                currentDuration -= Time.deltaTime;
            else
                Trigger();
        }

        private void StartTimer()
        {
            currentDuration = UnityEngine.Random.Range(timerDurationRange.x, timerDurationRange.y);
            enabled = true;
        }

        private void Trigger()
        {
            testAnimation.Play();
            onTimerExpired.Invoke();

            if (loop)
                StartTimer();
            else
                enabled = false;
        }

        public void PlayAnimation(Animation animation)
        {
            animation.Play();
        }
    }
}