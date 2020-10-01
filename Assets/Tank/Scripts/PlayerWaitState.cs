using System.Collections;
using UnityEngine;

public class PlayerWaitState : IEntityState
{
    private Coroutine enterStateCoroutine;
    private StateMachine playerController;
    private bool isWaiting;
    
    private WaitForSeconds wait = new WaitForSeconds(1f);
    
    public bool IsComplete { get; }
    public IEntityState NextState { get; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;
        isWaiting = true;
        enterStateCoroutine = playerController.StartCoroutine(WaitForAI());
        
        PlayerController.SendOnPlayerUIMessageUpdated("Waiting for AI");
    }

    public void OnExit() {
        
        if (enterStateCoroutine != null) {
            playerController.StopCoroutine(enterStateCoroutine);
            enterStateCoroutine = null;
        }
    }
    
    public void ProgressState() {
        throw new System.NotImplementedException();
    }

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
    
}