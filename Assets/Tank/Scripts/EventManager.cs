using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton Instance
    
    private static EventManager instance;
    
    #endregion
    
    #region Monobehaviour Methods
    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    #endregion

    #region Events

    public delegate void OnPayloadCollision(Vector3 hitLocation);

    public static event OnPayloadCollision onPayloadCollision;

    #endregion

    #region Event Methods

    public static void SendOnPayloadCollision(Vector3 hitPosition) {
        onPayloadCollision?.Invoke(hitPosition);
    }

    #endregion
}
