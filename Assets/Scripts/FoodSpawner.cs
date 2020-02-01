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
        Debug.Log(GameObject.FindGameObjectsWithTag("Food").Length);
        if (GameObject.FindGameObjectsWithTag("Food").Length < maxObjects)
        {
            int pointIndex = Random.Range(0, spawnPoints.Length);
            int itemIndex = Random.Range(0, foodItems.Length);

            Instantiate(foodItems[itemIndex], spawnPoints[pointIndex].transform);
        }
    }
}
