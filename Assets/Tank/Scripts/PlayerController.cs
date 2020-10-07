using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : StateMachine
{
    
    #region Singleton Instance

    public static PlayerController Instance => instance;

    private static PlayerController instance;

    #endregion
    
    #region Editor Properties

    public Collider GroundCollider;
    [SerializeField] private GameObject playerControlsUI;
    [SerializeField] private InputField angleInputField;
    [SerializeField] private InputField strengthInputField;
    [SerializeField] private Button launchButton;
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform shellspawn;
    
    [SerializeField] private GameObject playerWinUI;

    #endregion
    
    #region Fields and Properties
    
    public GameObject PlayerWinUI {
        get => playerWinUI;
    }
    
    public InputField AngleInputField {
        get => angleInputField;
    } 
    
    public InputField StrengthInputField {
        get => strengthInputField;
    } 
    
    public Button LaunchButton {
        get => launchButton;
    } 
    
    public GameObject PlayerControlsUI {
        get => playerControlsUI;
    } 
    
    private Vector3 selectedTargetLocation;

    public Vector3 SelectedTargetLocation {
        get => selectedTargetLocation;
    }

    private Vector3 targetLocation;

    public Vector3 TargetLocation {
        set => targetLocation = value;
    }

    private float launchAngle;

    public float LaunchAngle {
        get => launchAngle;
        set => launchAngle = value;
    }
    
    private float launchStrength;

    public float LaunchStrength {
        get => launchStrength;
        set => launchStrength = value;
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
        shellspawn.transform.LookAt(AIController.Instance.transform);
        shellspawn.transform.Rotate(Vector3.right, -launchAngle);
        
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
    
    public void LaunchPayload() {
        GameObject newShell = Instantiate(shellPrefab, shellspawn.position, shellspawn.rotation);
        
        Rigidbody shellRigidbody = newShell.GetComponent<Rigidbody>();
        
        newShell.transform.Rotate(Vector3.left, -launchAngle);
        
        shellRigidbody.velocity = shellspawn.forward * launchStrength;
    }

    #endregion
}
