using System.Collections;
using UnityEngine;

/// <summary>
/// A class representing the Wait State for the Player will progress on receiving the AI turn complete event
/// </summary>
public class PlayerWaitState : IEntityState
{
    #region Fields

    private Coroutine enterStateCoroutine;
    private PlayerController playerController;
    private bool isWaiting;
    
    private WaitForSeconds wait = new WaitForSeconds(1f);

    #endregion

    #region Constructors

    public PlayerWaitState(IEntityState nextState) {
        NextState = nextState;
    }

    #endregion
    
    #region IEntityState Implementation
    
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;
        AIController.onAITurnComplete += SetNoLongerWaiting;
        
        isWaiting = true;
        enterStateCoroutine = playerController.StartCoroutine(WaitForAI());
        
        playerController.SendOnPlayerUIMessageUpdated("Waiting for AI");
    }

    public void OnExit() {
        AIController.onAITurnComplete -= SetNoLongerWaiting;
        
        if (enterStateCoroutine != null) {
            playerController.StopCoroutine(enterStateCoroutine);
            enterStateCoroutine = null;
        }
    }
    
    public void ProgressState() {
        IsComplete = true;
    }
    
    #endregion

    #region Private Methods

    private IEnumerator WaitForAI() {
        while (isWaiting) {
            // Dont need to poll each frame so a slower yield is ok
            yield return wait;
        }
        ProgressState();
    }

    private void SetNoLongerWaiting() {
        isWaiting = false;
    }
    #endregion
    
}