using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GeneralManager : MonoBehaviour {

    private float score;

    public float Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public Transform Player;
    public GameObject PlateformManager;
    public Text scoreText;

    private bool isGameOver = false;

    public GameOverManager gameOverManager;

    void Start () {
        Score = 0;
	}
	
	void Update () {
        if (!isGameOver)
        {
            // Score is function of the position of the player
            Score = -Player.position.y * 40;
            scoreText.text = Mathf.Floor(Score).ToString();
            // Bonus points if the player is at the center of the plateforms
            if(Player.GetComponent<PlayerController>().IsInCombo)
            {
                Score += 10;
            }
        }
        // Game Over conditions
        if (Player.position.z < PlateformManager.GetComponent<PlateformInstantiation>().minimalZ || Player.GetComponent<PlayerController>().HitObstacle)
        {
            Player.GetComponent<PlayerController>().StopCurrentGame();
            gameOverManager.GameOver();
            isGameOver = true;
        }
	}

    // Called when the menu disappears
    public void StartGame()
    {
        Player.GetComponent<PlayerController>().WaitForStart = true;
        Score = 0;
        if (isGameOver)
        {
            isGameOver = false;
            Player.GetComponent<PlayerController>().HitObstacle = false;
            gameOverManager.PlayAgain();
        }
    }

    // Called when the "exit game" buttons is pressed
    public void ExitGame()
    {
        Application.Quit();
    }

    // Called when the "play again" button is pressed
    public void StartNewGame()
    {
        gameOverManager.StartGame();
        StartGame();
    }
}
