
using System.Collections;
using UnityEngine;

public class AIWaitState : IEntityState
{
    
    private Coroutine enterStateCoroutine;
    private StateMachine aiController;
    private bool isWaiting;
    
    public bool IsComplete { get; }
    public IEntityState NextState { get; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;
        isWaiting = true;
        enterStateCoroutine = aiController.StartCoroutine(WaitForPlayer());
    }

    public void OnExit() {
        
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
            yield return new WaitForEndOfFrame(); 
        }

        ProgressState();
    }

    private void SetNoLongerWaiting() {
        isWaiting = false;
    }
    
}
