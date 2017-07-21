using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
    public enum PatternType{StraightDown, CustomPoints}
    public PatternType patTypes;

    public float speed;
    public float rotSpeed;
    [Range(0,100)]
    public int dropPercent;
    public float yAdjuster;
    public GameObject[] powerUps;

    [System.Serializable]
    public struct CustomPoints
    {
        public Vector3[] patternPoints;
        public LayerMask enemiesLayerMask;
        public float radius;


        

        [Range(0, 10)]
        public float lerpSpeed;
    }
    public CustomPoints customP;

    
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
        if(patTypes == PatternType.CustomPoints)
        {
            CustomPointsType();
        }
        if(patTypes == PatternType.StraightDown)
        {
            StraightDownType();
        }

        //distruzione se esce dal bordo sotto
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)) + Vector3.up * yAdjuster;
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    
    void StraightDownType()
    {

    }

    private void CustomPointsType()
    {
        //controllo raggiunta obbiettivo
        if (Physics2D.OverlapCircle(customP.patternPoints[objectiveNumb], customP.radius, customP.enemiesLayerMask))
        {
            if (objectiveNumb != customP.patternPoints.Length - 1)
            {
                objectiveNumb++;
            }
            else
            {
                listFinished = true;
            }
        }

        //quando ha completato lista obbiettivi continua dritto, altrimenti ruota verso obbiettivo
        if (!listFinished)
        {
            Vector3 objDir = customP.patternPoints[objectiveNumb] - transform.position;
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
    }

    //visualizzazione
    private void OnDrawGizmosSelected()
    {
        if(patTypes == PatternType.CustomPoints)
        {
            VisualyzeCustomPointsMode();
        }
    }

    //visualizzazione modalità punti custom
    private void VisualyzeCustomPointsMode()
    {
        if (customP.patternPoints.Length > 0)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < customP.patternPoints.Length; i++)
            {
                Gizmos.DrawSphere(customP.patternPoints[i], 0.5f);

                if (i < customP.patternPoints.Length - 1)
                {
                    Gizmos.DrawLine(customP.patternPoints[i], customP.patternPoints[i + 1]);
                }
            }

            Vector3 objDir = customP.patternPoints[objectiveNumb] - transform.position;
            objDir.z = 0;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + objDir);
        }
    }
}
