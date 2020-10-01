using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : StateMachine
{
    
    #region Singleton Instance

    private static AIController instance;

    #endregion

    #region Editor Properties

    public Collider GroundCollider;
    [SerializeField] private GameObject shellPrefab;

    #endregion

    #region Fields and Properties

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

    #region Private Methods

    

    #endregion

    #region Public Methods
    
    public void SendOnAIUIMessageUpdated(string newMessage) {
        onAIUIMessageUpdated?.Invoke(newMessage);
    }
    
    public void SendOnAITurnComplete() {
        onAITurnComplete?.Invoke();
    }

    public void LaunchPayload() {
        //Instantiate()
    }

    #endregion
}
