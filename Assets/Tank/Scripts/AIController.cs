using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class to represent the controller of the computer player
/// </summary>
public class AIController : StateMachine
{
    
    #region Singleton Instance
    
    public static AIController Instance => instance;

    private static AIController instance;

    #endregion

    #region Editor Properties

    public Collider GroundCollider;
    [SerializeField] private GameObject shellPrefab;
    [SerializeField] private Transform shellspawn;
    
    [SerializeField] private GameObject aIWinUI;

    #endregion

    #region Fields and Properties

    public GameObject AIWinUI {
        get => aIWinUI;
    }

    private Vector3 targetMovementLocation;

    public Vector3 TargetMovementLocation {
        set => targetMovementLocation = value;
    }
    
    private NavMeshAgent navMeshAgent;

    [NonSerialized]
    public List<float> LaunchAngles = new List<float>();
    
    [NonSerialized]
    public List<float> LaunchStrengths = new List<float>();
    
    [NonSerialized]
    public List<float> LaunchHitToTargetDistances = new List<float>();
    
    
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
        shellspawn.transform.LookAt(PlayerController.Instance.transform);
        if (LaunchAngles.Count > 0) {
            shellspawn.transform.Rotate(Vector3.right, -LaunchAngles[LaunchAngles.Count - 1]);
        }
        
        if (CurrentState.GetType() == typeof(AIMovementState)) {
            transform.LookAt(targetMovementLocation);

            navMeshAgent.destination = targetMovementLocation;
            
            if (Vector3.Distance(transform.position, targetMovementLocation) < 0.5f) {
                navMeshAgent.destination = transform.position;
                transform.position = targetMovementLocation;

                CurrentState.ProgressState();
            }
        }
    }

    #endregion

    #region Public Methods
    
    /// <summary>
    /// Sends the AIUI update string to any listening UI components
    /// </summary>
    public void SendOnAIUIMessageUpdated(string newMessage) {
        onAIUIMessageUpdated?.Invoke(newMessage);
    }
    
    /// <summary>
    /// Sends the AI Turn Complete event to any listening player states
    /// </summary>
    public void SendOnAITurnComplete() {
        onAITurnComplete?.Invoke();
    }

    /// <summary>
    /// Launches the payload based on the last calculated components from the Solution Acquisition state
    /// </summary>
    public void LaunchPayload() {
        GameObject newShell = Instantiate(shellPrefab, shellspawn.position, shellspawn.rotation);
        
        Rigidbody shellRigidbody = newShell.GetComponent<Rigidbody>();
        
        newShell.transform.Rotate(Vector3.left, -LaunchAngles[LaunchAngles.Count - 1]);
        
        shellRigidbody.velocity = shellspawn.forward * LaunchStrengths[LaunchStrengths.Count - 1];
    }

    #endregion
}
