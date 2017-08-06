using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(newEnemy))]
public class EnemyEditor : Editor
{
    private newEnemy enemy;

    private Transform tempTransform;
    private Quaternion tempRotation;

    private const float hendleSize = 0.1f;
    private const float pickSize = 0.5f;

    private int currentIndex = -1;
    private int pathSteps = 10;

    private void OnSceneGUI()
    {
        enemy = target as newEnemy;

        tempTransform = enemy.transform;
        tempRotation = Tools.pivotRotation == PivotRotation.Local ?
            tempTransform.rotation : Quaternion.identity;

        // generate point
       for(int i = 0; i < enemy.pathPoints.Capacity - 1; i++)
        {
            Vector3 point1 = ShowPoint(i);
            Vector3 point2 = ShowPoint(i + 1);
            Handles.color = Color.grey;
            Handles.DrawLine(point1, point2);
        }
       for(int i = 0; i < enemy.pathPoints.Capacity ; i+=3)
        {
            Vector3 p01 = enemy.pathPoints[i];
            Vector3 p02 = enemy.pathPoints[i + 1];
            Vector3 p03 = enemy.pathPoints[i + 2];

            Vector3 first = enemy.getPoint(p01, p02, p03, 0f);

            for(int a = 1; a <= pathSteps; a++)
            {
                Handles.color = Color.red;
                Vector3 curve = enemy.getPoint(p01, p02, p03, a / (float)pathSteps);
                Handles.DrawLine(first, curve);
                first = curve;
            }


        }

       
    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = tempTransform.TransformPoint(enemy.pathPoints[index]);
        Handles.color = Color.green;

        if(Handles.Button(point, tempRotation, hendleSize,pickSize, Handles.DotCap))
        {
            currentIndex = index;
        }


        if(currentIndex == index)
        {

            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, tempRotation);
            if (EditorGUI.EndChangeCheck())
            {
                enemy.pathPoints[index] = tempTransform.InverseTransformPoint(point);
            }
        }
        return point;
    }



}
