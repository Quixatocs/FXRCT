
using UnityEngine;

public static class Vector3Extensions
{
    public static bool Approximately(this Vector3 thisVector3, Vector3 otherVector3) {
        return Mathf.Approximately(thisVector3.x, otherVector3.x) &&
               Mathf.Approximately(thisVector3.y, otherVector3.y) &&
               Mathf.Approximately(thisVector3.z, otherVector3.z);
    }
}
