using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionWithGroundEvent : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        EventManager.SendOnPayloadCollision(other.transform.position);
    }
}
