using UnityEngine;

/// <summary>
/// A monobehaviour that controls the collision of a payload with the ground collider
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class CollisionWithGroundEvent : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        GetComponent<AudioSource>().Play();
        EventManager.SendOnPayloadCollision(other.transform.position);
        
        Destroy(other.gameObject, 0.1f);
    }
}
