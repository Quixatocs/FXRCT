using System.Collections;
using UnityEngine;

/// <summary>
/// A class representing the Wait State for the AI will progress on receiving the player turn complete event
/// </summary>
public class AIWaitState : IEntityState
{
    #region Fields

    private Coroutine enterStateCoroutine;
    private AIController aiController;
    private bool isWaiting;
    
    private WaitForSeconds wait = new WaitForSeconds(1f);

    #endregion
    
    #region IEntityState Implementation
    
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;
        PlayerController.onPlayerTurnComplete += SetNoLongerWaiting;
        
        isWaiting = true;
        enterStateCoroutine = aiController.StartCoroutine(WaitForPlayer());
        
        aiController.SendOnAIUIMessageUpdated("Waiting for Player");
    }

    public void OnExit() {
        PlayerController.onPlayerTurnComplete -= SetNoLongerWaiting;
        
        if (enterStateCoroutine != null) {
            aiController.StopCoroutine(enterStateCoroutine);
            enterStateCoroutine = null;
        }
    }
    
    public void ProgressState() {
        NextState = new AISolutionAcquisitionState();
        IsComplete = true;
    }
    
    #endregion

    #region Private Methods

    /// <summary>
    /// Waits to receive the signal that the player has completed their turn
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForPlayer() {
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
