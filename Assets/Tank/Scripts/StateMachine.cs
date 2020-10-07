using System.Collections;
using UnityEngine;

/// <summary>
/// Class representing the abstract statemachine that runs through various individual IEntityStates
/// </summary>
public abstract class StateMachine : MonoBehaviour
{
    #region State Machine Fields

    private IEntityState currentState;

    public IEntityState CurrentState => currentState;

    private Coroutine stateCompleteCheck;

    #endregion

    #region State Machine Methods

    /// <summary>
    /// Sets the next state in the state machine, clears up the previous state
    /// and calls OnEnter for the new state
    /// </summary>
    protected void SetState(IEntityState nextState) {
        
        // Exit previous state
        if (currentState != null) {
            currentState.OnExit();
        }
        
        currentState = nextState;
        
        if (currentState != null) {
            currentState.OnEnter(this);
            
            if (stateCompleteCheck == null) {
                stateCompleteCheck = StartCoroutine(CheckComplete());
            }
        }
        else {
            if (stateCompleteCheck != null) {
                StopCoroutine(stateCompleteCheck);
                stateCompleteCheck = null;
            }
        }
    }

    /// <summary>
    /// Checks if the current state is complete
    /// </summary>
    private IEnumerator CheckComplete() {
        while (true) {
            yield return new WaitForEndOfFrame();
            
            if (currentState == null) { 
                // We are leaving the state machine so we can stop running the check
                stateCompleteCheck = null;
                yield break;
            }
            
            if (currentState.IsComplete) {
                SetState(currentState.NextState);
            }
        }
    }

    #endregion
}
