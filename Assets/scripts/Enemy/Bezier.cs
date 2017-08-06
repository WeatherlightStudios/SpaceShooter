using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 getBezierLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return Vector3.Lerp(a, c , t);
    }
}