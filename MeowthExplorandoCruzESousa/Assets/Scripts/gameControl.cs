using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameControl : MonoBehaviour
{
    [SerializeField]
    public KeyCode keyRestart;
    public KeyCode keyMiniGame;

    public minigameLogic miniGame;

    void Update()
    {
        if (Input.GetKey(keyRestart))
            Restart();
        if (Input.GetKey(keyMiniGame))
            miniGame.EndMiniGame();
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
