using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{


    public GameObject starObj;


    public float min_star_scale, max_star_scale;
    public int maxStar;


    public float max_speed, min_speed;

	void Start ()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


		for(int i = 0; i < maxStar; i++)
        {


            GameObject star = Instantiate(starObj);


            star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            float scale = Random.Range(min_star_scale , max_star_scale);

            star.transform.localScale = new Vector3(scale , scale , scale);
            star.GetComponent<Star>().speed = Random.Range(max_speed, min_speed) * scale;
            star.transform.parent = transform;
        }
	}
	
	
	void Update ()
    {
		
	}
}
