
using UnityEngine;

public class AIFireState : IEntityState
{
    private AIController aiController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;

        EventManager.onPayloadCollision += AnalyseImpactDistance;
        
        aiController.SendOnAIUIMessageUpdated("Payload Launching");
        aiController.LaunchPayload();
    }

    public void OnExit() {
        EventManager.onPayloadCollision -= AnalyseImpactDistance;
    }

    public void ProgressState() {
        NextState = new AIWaitState();
        IsComplete = true;
        aiController.SendOnAITurnComplete();
    }

    public void AnalyseImpactDistance(Vector3 hitLocation) {
        float distance = Vector3.Distance(hitLocation, PlayerController.Instance.transform.position);
        Debug.Log($"AI payload Distance to Player : {distance}");
        aiController.LaunchHitToTargetDistances.Add(distance);

        if (distance < 2f) {
            NextState = new AIWinState();
            IsComplete = true;
            return;
        }
        
        ProgressState();
    }
}
