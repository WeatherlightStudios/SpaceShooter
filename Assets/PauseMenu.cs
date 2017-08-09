using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public Canvas canvas;
    public float timer;
    
    float _timer;
    float startTime;
    float survivedTime;
    GameObject timerText;
    GameObject scoreText;
    float playerScore;

    bool once = true;

	// Use this for initialization
	void Start () {
        canvas.enabled = false;

        _timer = timer;

        startTime = Time.time;
        
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.Find("Player");

        
        
        if (player == null)
        {
            _timer -= Time.deltaTime;

            if (once)
            {
                survivedTime = Mathf.RoundToInt(Time.time - startTime);
                
                timerText = GameObject.Find("Text_Timer");
                timerText.GetComponent<Text>().text = "You survived for: " + survivedTime + " seconds";


                scoreText = GameObject.Find("Text_Score");
                scoreText.GetComponent<Text>().text = "Your score is: " + playerScore + " points";

                once = false;
            }
            
        }
        else
        {
            //TODO: questo usa troppa performance, cambia
            playerScore = player.GetComponent<Player>().score;
        }

        if(player == null)
        {
            
        }

        if (_timer <= 0)
        {
            canvas.enabled = true;
        }
	}
}
