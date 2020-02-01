using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public int maxObjects = 5;
    private GameObject[] spawnPoints;
    public GameObject[] foodItems;

    public float spawnInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        InvokeRepeating("SpawnRandom", 0f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnRandom()
    {
        if (GameObject.FindGameObjectsWithTag("Food").Length < maxObjects)
        {
            int pointIndex = Random.Range(0, spawnPoints.Length);
            int itemIndex = Random.Range(0, foodItems.Length);

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(spawnPoints[pointIndex].transform.position, 0.2f);
            Debug.Log(hitColliders.Length);
            foreach(Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Food")
                {
                    Debug.Log("There is already food at this point, spawning elsewhere");
                    SpawnRandom();
                    return;
                }
            }

            Instantiate(foodItems[itemIndex], spawnPoints[pointIndex].transform);
        }
    }
}
