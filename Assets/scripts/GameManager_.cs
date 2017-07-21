using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ : MonoBehaviour
{
    public GameObject player;

    public GameObject enemy;

    public Vector3 player_spawn;

    bool is_game_star;

    public int score;

    public float yAdjuster;


    


	// Use this for initialization
	void Start () {
        //InvokeRepeating("spawn_enemy", 5.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}





    void spawn_enemy()
    {
        GameObject obj = Instantiate(enemy);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) + Vector3.up * yAdjuster;


        obj.transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }


}
