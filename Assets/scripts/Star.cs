using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    public float speed;


	
	// Update is called once per frame
	void Update ()
    {

        Vector2 bottom_left = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 top_left = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 top_right = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Vector2 pos = transform.position;



        pos = new Vector2(pos.x , pos.y + speed * Time.deltaTime);

        transform.position = pos;



        if (transform.position.y < bottom_left.y)
        {
            transform.position = new Vector2(Random.Range(top_left.x, top_right.x), Random.Range(top_left.y - 0.5f, top_right.y - 0.5f));
        }


    }
}
