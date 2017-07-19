using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float anim_move;

    public GameObject spown_shoot;
    public GameObject bullet;

    public float fireRate = 0.0f;
    float nextFire = 0.0f;



	void Start ()
    {
		
	}
	

	void FixedUpdate ()
    {
        float movimentHorizontal = Input.GetAxisRaw("Horizontal");
        float movimentVertical = Input.GetAxisRaw("Vertical");


        Vector2 movement = new Vector2(movimentHorizontal, movimentVertical);



        if(Input.GetButton("Jump") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            GameObject m_bullet = Instantiate(bullet);
            m_bullet.transform.position = spown_shoot.transform.position;

        }



        Move(movement);
        animationMovement(anim_move);
    }



    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));


        Vector2 pos = transform.position;

        pos += direction.normalized * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    void animationMovement(float anim_int)
    {
        float movimentHorizontal = Input.GetAxis("Horizontal");
        float movimentVertical = Input.GetAxis("Vertical");



        Quaternion pos = transform.rotation;

        pos = Quaternion.Euler(0, movimentHorizontal * anim_int * -1, 0);

        transform.rotation = pos;
    }
}
