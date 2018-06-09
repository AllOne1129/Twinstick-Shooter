using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MainMenuBehaviour {
    public static bool Paused;
    public GameObject PauseMenu;
	// Use this for initialization
	public void Start () {
        Time.timeScale = 1;
        Paused = false;
        PauseMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp("escape"))
        {
            Paused = !Paused;
            Time.timeScale = (Paused) ? 0 : 1;
            PauseMenu.SetActive(Paused);
        }
	}

    public void ResumeGame()
    {
        Paused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
