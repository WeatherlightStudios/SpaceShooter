using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float anim_move;

    public float score;
    
    [System.Serializable]
    public class CustomizeBullets
    {
        public GameObject spawn_shoot;
        public GameObject bullet;

        public float fireRate = 0.0f;
        [Range(1,5)]
        public int bulletNumber;
        [Range(0f,3f)]
        public float bulletDistance;
        [Range(0f,45f)]
        public float bulletAngle;

        [System.NonSerialized]
        public float nextFire = 0.0f;

        [System.NonSerialized]
        public float currentAngle;

        [System.NonSerialized]
        public float currentDistance;
    }
    public CustomizeBullets bullets;



	void Start ()
    {
		
	}
	

	void FixedUpdate ()
    {
        float movimentHorizontal = Input.GetAxisRaw("Horizontal");
        float movimentVertical = Input.GetAxisRaw("Vertical");


        Vector2 movement = new Vector2(movimentHorizontal, movimentVertical);



        if(Input.GetButton("Jump") && Time.time > bullets.nextFire)
        {
            bullets.nextFire = Time.time + bullets.fireRate;

            
            //la distanza minima da cui partire è distanza fra ogni proiettile per ogni proiettile - 1 /2 partendo da sinistra
            bullets.currentDistance = bullets.bulletDistance * (bullets.bulletNumber - 1) / 2;

            //l'angolo da cui partire è angolo fra ogni proiettile per ogni proiettile - 1 / 2 partendo da sinistra
            bullets.currentAngle = bullets.bulletAngle * (bullets.bulletNumber - 1) / 2;

            for (int i = 0; i < bullets.bulletNumber; i++)
            {
                Vector3 bulletPos = bullets.spawn_shoot.transform.position - Vector3.left * bullets.currentDistance;
                //Quaternion bulletRot = bullets.bullet.transform.rotation
                GameObject m_bullet = Instantiate(bullets.bullet, bulletPos, transform.rotation);
                m_bullet.transform.Rotate(Vector3.forward * bullets.currentAngle * -1 ); //* Mathf.Deg2Rad


                bullets.currentDistance -= bullets.bulletDistance;
                bullets.currentAngle -= bullets.bulletAngle;
                //m_bullet.transform.position = bullets.spawn_shoot.transform.position;
            }

            

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
        //per ora tienilo
        //float movimentVertical = Input.GetAxis("Vertical");



        Quaternion pos = transform.rotation;

        pos = Quaternion.Euler(0, movimentHorizontal * anim_int * -1, 0);

        transform.rotation = pos;
    }

    
}
