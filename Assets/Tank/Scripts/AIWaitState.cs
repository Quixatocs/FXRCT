using System.Collections;
using UnityEngine;

public class AIWaitState : IEntityState
{
    
    private Coroutine enterStateCoroutine;
    private AIController aiController;
    private bool isWaiting;
    
    private WaitForSeconds wait = new WaitForSeconds(1f);
    
    public bool IsComplete { get; }
    public IEntityState NextState { get; }
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
        throw new System.NotImplementedException();
    }

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
    
}
