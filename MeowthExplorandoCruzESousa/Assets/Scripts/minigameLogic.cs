using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class minigameLogic : MonoBehaviour
{
    private float time;
    private int minutes;
    private int seconds;
    private int millis;
    public Text display;

    public GameObject[] checkpoints;
    private int counterCP;

    private bool gameRunning;

    void Start()
    {
        gameRunning = true;
        time = 0f;
        for (int i = 1; i < checkpoints.Length; i++)
            checkpoints[i].SetActive(false);
        counterCP = 0;
        checkpoints[0].SetActive(true);
    }

    public void EndMiniGame()
    {
        gameRunning = false;
        display.text = "";
        for (int i = 0; i < checkpoints.Length; i++)
            checkpoints[i].SetActive(false);
    }

    void FixedUpdate()
    {
        if (gameRunning)
        {
            time += Time.deltaTime;
            minutes = (int) time / 60;
            seconds = (int) time % 60;

            display.text = string.Format("{00:00}:{01:00}", minutes, seconds);
        }
    }

    void OnEnable()
    {
        checkpointLogic.OnEnter += checkpointAction;
    }

    void OnDisable()
    {
        checkpointLogic.OnEnter -= checkpointAction;
    }

    void checkpointAction()
    {
        checkpoints[counterCP].SetActive(false);
        counterCP++;
        if (counterCP < checkpoints.Length)
        {
            checkpoints[counterCP].SetActive(true);
        } else
        {
            gameRunning = false;
        }
    }
}

