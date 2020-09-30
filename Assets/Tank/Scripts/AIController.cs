
using System;
using System.Collections;
using UnityEngine;

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
        instance.SetState(new AIMovementState());
    }

    private void Update() {
        if (CurrentState.GetType() == typeof(AIMovementState)) {
            transform.LookAt(targetLocation);
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, 10f * Time.deltaTime);
            if (transform.position.Approximately(targetLocation)) {
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
