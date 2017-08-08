using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAndRotateAnimationShips : MonoBehaviour {

    public AnimationCurve heightCurve;
    public float heightAdd;
    public Vector3 startPos;
    public float rotSpeed;

    private float minHeight;
   
	// Use this for initialization
	void Start () {
        minHeight = transform.position.y;
    }



	

	// Update is called once per frame
	void Update () {
        //float up and down
        float newHeight = minHeight + heightCurve.Evaluate(Time.time) * heightAdd;
        Vector3 newPos = new Vector3(transform.position.x, newHeight, transform.position.z);
        transform.position = newPos;

        //rotate over time
        transform.Rotate(new Vector3(0,0,1) * rotSpeed * Time.deltaTime);
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPos, 1);
        Gizmos.DrawWireSphere(startPos + Vector3.up * heightAdd, 1);
    }

}
