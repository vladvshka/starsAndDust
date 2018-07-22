using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	public void PlayGame()
    {
        SceneManager.LoadScene("mainTrainingScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void Continue()
    {
        if((SceneManager.GetActiveScene().buildIndex + 1) == SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene("MainMenuScene");   
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
