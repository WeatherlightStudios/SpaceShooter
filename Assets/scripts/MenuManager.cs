using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("test_scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
