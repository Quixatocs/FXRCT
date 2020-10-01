using UnityEngine;

public class LookInDirectionOfMotion : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Vector3.forward);
    }
}
