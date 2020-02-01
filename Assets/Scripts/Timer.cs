using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Gradient skyColor;

    public float totalTime = 10f;
    private float timeRemaining;
    private bool gameEnded;

    private Transform timerBar;
    private Camera camera;

    private Transform dayNightCycle;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = totalTime;
        timerBar = GameObject.Find("TimerBar").transform;
        camera = Camera.main;

        dayNightCycle = GameObject.Find("DayNightCycle").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0f)
        {
            dayNightCycle.Rotate(new Vector3(0f, 0f, (Time.deltaTime / totalTime) * 90f));

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

        SceneManager.LoadScene("JanitorWon");
    }
}
