using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	
    public enum PatternType{StraightDown, CustomPoints}
    public PatternType patTypes;

    public float speed;
    public float rotSpeed;

    [System.Serializable]
    public class CustomPoints
    {
        public Vector3[] patternPoints;
        public LayerMask enemiesLayerMask;
        public float radius;

        [System.NonSerialized]
        public int objectiveNumb;
        [System.NonSerialized]
        public float closeObjective;
        [System.NonSerialized]
        public bool listFinished = false;

        [Range(0, 10)]
        public float lerpSpeed;
    }
    public CustomPoints customP;
    
    
    //inizializzazione
    void Start()
    {
        customP.objectiveNumb = 0;
    }

    void Update ()
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
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    
    void StraightDownType()
    {

    }

    void CustomPointsType()
    {
        //controllo raggiunta obbiettivo
        if (Physics2D.OverlapCircle(customP.patternPoints[customP.objectiveNumb], customP.radius, customP.enemiesLayerMask))
        {
            if (customP.objectiveNumb != customP.patternPoints.Length - 1)
            {
                customP.objectiveNumb++;
            }
            else
            {
                customP.listFinished = true;
            }
        }

        //quando ha completato lista obbiettivi continua dritto, altrimenti ruota verso obbiettivo
        if (!customP.listFinished)
        {
            Vector3 objDir = customP.patternPoints[customP.objectiveNumb] - transform.position;
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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    //visualizzazione
    void OnDrawGizmosSelected()
    {
        if(patTypes == PatternType.CustomPoints)
        {
            VisualyzeCustomPointsMode();
        }
    }

    //visualizzazione modalità punti custom
    void VisualyzeCustomPointsMode()
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

            Vector3 objDir = customP.patternPoints[customP.objectiveNumb] - transform.position;
            objDir.z = 0;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, transform.position + objDir);
        }
    }
}
