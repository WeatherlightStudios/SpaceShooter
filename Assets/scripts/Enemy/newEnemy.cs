using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemy : MonoBehaviour
{

    public List<Vector3> pathPoints = new List<Vector3>();




    public Vector3 getPoint(Vector3 p01, Vector3 p02, Vector3 p03, float index)
    {
        return transform.TransformPoint(Bezier.getBezierLerp(p01,p02,p03,index));
    }


    public void Reset()
    {
        for(int i = 0; i < pathPoints.Capacity; i++)
        {
            pathPoints[i] = new Vector3(i + 1, 0f, 0f);
        }
    }

}
