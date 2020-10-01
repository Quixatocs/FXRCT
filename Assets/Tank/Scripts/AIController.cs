using UnityEngine;
using UnityEngine.AI;

public class AIController : StateMachine
{
    
    #region Singleton Instance

    private static AIController instance;

    #endregion

    #region Editor Properties

    [SerializeField] public Collider GroundCollider;

    #endregion

    #region Fields and Properties

    private Vector3 targetLocation;

    public Vector3 TargetLocation {
        set => targetLocation = value;
    }

    private NavMeshAgent navMeshAgent;
    
    public delegate void OnAIUIMessageUpdated(string newMessage);
    public static event OnAIUIMessageUpdated onAIUIMessageUpdated;
    
    public delegate void OnAITurnComplete(); 
    public static event OnAITurnComplete onAITurnComplete;

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

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        instance.SetState(new AIMovementState());
    }

    private void Update() {
        if (CurrentState.GetType() == typeof(AIMovementState)) {
            transform.LookAt(targetLocation);

            navMeshAgent.destination = targetLocation;
            
            if (Vector3.Distance(transform.position, targetLocation) < 0.5f) {
                navMeshAgent.destination = transform.position;
                transform.position = targetLocation;

                CurrentState.ProgressState();
            }
        }
    }

    #endregion

    #region Private Methods

    

    #endregion

    #region Public Methods
    
    public void SendOnAIUIMessageUpdated(string newMessage) {
        onAIUIMessageUpdated?.Invoke(newMessage);
    }
    
    public void SendOnAITurnComplete() {
        onAITurnComplete?.Invoke();
    }

    #endregion
}
