using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public Canvas canvas;
    public float timer;

    float _timer;
	// Use this for initialization
	void Start () {
        canvas.enabled = false;

        _timer = timer;
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.Find("Player");

        if(player == null)
        {
            _timer -= Time.deltaTime;
        }

        if(_timer <= 0)
        {
            canvas.enabled = true;
        }
	}
}
