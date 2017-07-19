using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	void Start () {
		
	}


    public float speed;

	void Update ()
    {


        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 pos = transform.position;



        pos = new Vector2(pos.x, pos.y + speed * Time.deltaTime);

        transform.position = pos;

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
        // Debug.Log("Trigger");

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_bullet")
            Destroy(gameObject);
    }
}
