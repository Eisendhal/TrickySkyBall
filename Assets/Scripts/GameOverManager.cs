using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
	}

    // Trigger the GameOver animation
    public void GameOver()
    {
        anim.SetTrigger("GameOver");
    }

    // Trigger the StartGame animation
    public void StartGame()
    {
        anim.SetTrigger("StartGame");
    }

    // Trigger the PlayAgain animation and reload the scene
    public void PlayAgain()
    {
        anim.SetTrigger("PlayAgain");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
