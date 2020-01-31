using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Gradient skyColor;

    public float totalTime = 10f;
    private float timeRemaining;
    private bool gameEnded;

    private Transform timerBar;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = totalTime;

        timerBar = GameObject.Find("TimerBar").transform;

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            float progress = (totalTime - timeRemaining) / totalTime;
            timerBar.localScale = new Vector3(progress, 1f, 1f);

            camera.backgroundColor = skyColor.Evaluate(progress);
        } else if (!gameEnded)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
    }
}
