﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    //public Rigidbody2D rb;


	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //per ora tienilo, può servire
        //Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


        //Vector2 pos = transform.position;

        //pos = new Vector2(pos.x, pos.y + speed * Time.deltaTime);

        speed += 100 * Time.deltaTime;

        transform.position = transform.position + transform.up * speed * Time.deltaTime;

        //rb.velocity = Vector3.up * speed * Time.deltaTime;


        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }
}
