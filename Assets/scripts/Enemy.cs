using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CustomPathPoints
{
    public Vector3[] patternPathPoints;
    public LayerMask enemiesLayerMask;
    public float radius;


        

    [Range(0, 10)]
    public float lerpSpeed;
}
public class Enemy : MonoBehaviour
{
	
    public enum PatternType{StraightDown, CustomPathPoints}
    public PatternType patTypes;

    public float speed;
    public float rotSpeed;
    [Range(0,100)]
    public int dropPercent;
    public float yAdjuster;
    public GameObject[] powerUps;

    public CustomPathPoints customP;

    float collisionBox_size = 0.5f;
    
    private int objectiveNumb;
    
    private bool listFinished;

    //inizializzazione
    private void Start()
    {
        listFinished = false;
        objectiveNumb = 0;
    }

    private void Update ()
    {
        //continua a muoversi in avanti
        transform.position = transform.position + transform.up * speed * Time.deltaTime;
        
        //tipologia movimento = punti custom
        if(patTypes == PatternType.CustomPathPoints)
        {
            CustomPathPointsType();
        }
        if(patTypes == PatternType.StraightDown)
        {
            StraightDownType();
        }

        //distruzione se esce dal bordo sotto

        //Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Vector3.up * yAdjuster;
        //if (transform.position.y < min.y)
        //{
        //    Destroy(gameObject);
        //}
    }
    
    void StraightDownType()
    {

    }

    private void CustomPathPointsType()
    {
        //controllo raggiunta obbiettivo


        if(transform.position.x < this.customP.patternPathPoints[objectiveNumb].x + collisionBox_size && transform.position.x > this.customP.patternPathPoints[objectiveNumb].x - collisionBox_size)
        {
            if (this.objectiveNumb != this.customP.patternPathPoints.Length - 1)
            {
                this.objectiveNumb++;
            }
            else
            {
                listFinished = true;
            }
        }



        //if (Physics2D.OverlapCircle(this.customP.patternPathPoints[objectiveNumb], this.customP.radius, this.customP.enemiesLayerMask))
        //{
        //}

        //quando ha completato lista obbiettivi continua dritto, altrimenti ruota verso obbiettivo
        if (!listFinished)
        {
            Vector3 objDir = customP.patternPathPoints[objectiveNumb] - transform.position;
            float angle = Mathf.Atan2(objDir.y, objDir.x) * Mathf.Rad2Deg - 90;
            //transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * customP.lerpSpeed);
        }
    }

    //distruzione proiettile e bersaglio quando colpito
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player_bullet")
        {
            int randomNumb = Random.Range(0, 100);

            if(randomNumb < dropPercent)
            {
                int randomObject = Random.Range(0, powerUps.Length - 1);
                Instantiate(powerUps[randomObject], transform.position, transform.rotation);
            }

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        if(other.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        if(other.tag == "DestroyZone")
        {
            Destroy(this.gameObject);
        }

    }

    //visualizzazione
    private void OnDrawGizmosSelected()
    {
        if(patTypes == PatternType.CustomPathPoints)
        {
            VisualyzeCustomPathPointsMode();
        }
    }

    //visualizzazione modalità punti custom
    private void VisualyzeCustomPathPointsMode()
    {
        if (customP.patternPathPoints.Length > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < customP.patternPathPoints.Length; i++)
            {
                Gizmos.DrawSphere(customP.patternPathPoints[i], 0.5f);

                if (i < customP.patternPathPoints.Length - 1)
                {
                    Gizmos.DrawLine(customP.patternPathPoints[i], customP.patternPathPoints[i + 1]);
                }
            }

            Vector3 objDir = customP.patternPathPoints[objectiveNumb] - transform.position;
            objDir.z = 0;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + objDir);
        }
    }
}
