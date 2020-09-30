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

    #region Fields

    public Vector3 targetLocation;
    private NavMeshAgent navMeshAgent;

    #endregion
    
    #region Monobehaviour Methods

    private void Awake() {
        // Singleton implementation
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
                Debug.Log("GOT THERE");
                navMeshAgent.destination = transform.position;
                transform.position = targetLocation;

                (CurrentState as AIMovementState).ReadyForNextState();
            }
        }
    }

    #endregion

    #region Private Methods

    

    #endregion

    #region Public Methods
    
    

    #endregion
}
