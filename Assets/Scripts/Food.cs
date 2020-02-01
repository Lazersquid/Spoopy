using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Food : MonoBehaviour
    {
        public int energy = 50;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0f, 0f, 0.5f * Mathf.Sin(Time.time * 5f))); ;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Repairer repairer = collision.gameObject.GetComponent<Repairer>();
            if (repairer != null)
            {
                repairer.ChangeEnergy(energy);
                Destroy(gameObject);
            }
        }
    }
}