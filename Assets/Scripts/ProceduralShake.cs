using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ProceduralShake : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float amount = 1f;
        [SerializeField] private float duration = 0.1f;
        [SerializeField] private GameObject targetObject;
        private float remainingDuration;
        private Vector3 startingPosition;
        

        private void Awake()
        {
            startingPosition = targetObject.transform.localPosition;
        }

        private void Update()
        {
            if (remainingDuration >= 0f)
            {
                remainingDuration -= Time.deltaTime;
                targetObject.transform.localPosition = new Vector3(
                    startingPosition.x + Mathf.Sin(Time.time * speed) * amount,
                    startingPosition.y + Mathf.Sin(Time.time * speed) * amount,
                    startingPosition.z
                    );

                if (remainingDuration <= 0f)
                    targetObject.transform.localPosition = startingPosition;
            }
        }

        public void Shake()
        {
            remainingDuration = duration;
        }
    }
}