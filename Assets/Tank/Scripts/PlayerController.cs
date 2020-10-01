using UnityEngine;
using UnityEngine.AI;

public class PlayerController : StateMachine
{
    
    #region Singleton Instance

    private static PlayerController instance;

    #endregion
    
    #region Editor Properties

    public Collider GroundCollider;
    [SerializeField] private GameObject shellPrefab;

    #endregion
    
    #region Fields and Properties
    
    private Vector3 selectedTargetLocation;

    public Vector3 SelectedTargetLocation {
        get => selectedTargetLocation;
    }

    private Vector3 targetLocation;

    public Vector3 TargetLocation {
        set => targetLocation = value;
    }

    private NavMeshAgent navMeshAgent;
    
    public delegate void OnPlayerUIMessageUpdated(string newMessage);
    public static event OnPlayerUIMessageUpdated onPlayerUIMessageUpdated;
    
    public delegate void OnPlayerTurnComplete(); 
    public static event OnPlayerTurnComplete onPlayerTurnComplete;

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
        instance.SetState(new PlayerLocationSelectState());
    }
    
    private void Update() {

        if (CurrentState.GetType() == typeof(PlayerLocationSelectState)) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                    if (hit.collider == GroundCollider) {
                        selectedTargetLocation = hit.point;
                        
                        CurrentState.ProgressState();
                    }
                }
            }
        }
        
        if (CurrentState.GetType() == typeof(PlayerMovementState)) {
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
    
    public void SendOnPlayerUIMessageUpdated(string newMessage) {
        onPlayerUIMessageUpdated?.Invoke(newMessage);
    }
    
    public void SendOnPlayerTurnComplete() {
        onPlayerTurnComplete?.Invoke();
    }

    #endregion
}
