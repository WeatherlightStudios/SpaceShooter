using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	


    public float speed;
    public Vector3[] patternPoints;
    public LayerMask enemiesLayerMask;
    public float radius;
    public float rotSpeed;

    int objectiveNumb;
    float closeObjective;
    bool listFinished = false;

    void Start()
    {
        objectiveNumb = 0;
    }

    void Update ()
    {


        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        Vector2 pos = transform.position;

        
        Vector3 _pos = transform.localPosition;

        
        transform.position = transform.position + transform.up * speed * Time.deltaTime;


        Vector3 currentDir = transform.forward;
        Vector3 objDir = patternPoints[objectiveNumb] - transform.position;
        float angle = Mathf.Atan2(objDir.y, objDir.x) * Mathf.Rad2Deg - 90;


       

        if (Physics2D.OverlapCircle(patternPoints[objectiveNumb], radius, enemiesLayerMask))
        {
            if(objectiveNumb != patternPoints.Length - 1)
            {
                objectiveNumb++;
            }
            else
            {
                listFinished = true;
            }
        }

        if (!listFinished)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player_bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < patternPoints.Length; i++)
        {
            Gizmos.DrawSphere(patternPoints[i], 0.5f);

            if (i < patternPoints.Length - 1)
            {
                Gizmos.DrawLine(patternPoints[i], patternPoints[i + 1]);
            }
        }

        Vector3 objDir = patternPoints[objectiveNumb] - transform.position;
        objDir.z = 0;
        Gizmos.DrawLine(transform.position, transform.position + objDir);
    }
}
