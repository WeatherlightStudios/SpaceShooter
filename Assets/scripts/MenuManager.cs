﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndlessMode");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
