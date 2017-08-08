using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndlessMode");
    }

    public void PlayHangar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Hangar_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }


}
