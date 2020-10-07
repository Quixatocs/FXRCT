using UnityEngine;

public class PlayerFireState : IEntityState
{
    private PlayerController playerController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;

        EventManager.onPayloadCollision += AnalyseImpactDistance;
        
        playerController.SendOnPlayerUIMessageUpdated("Payload Launching");
        playerController.LaunchPayload();
    }

    public void OnExit() {
        EventManager.onPayloadCollision -= AnalyseImpactDistance;
    }

    public void ProgressState() {
        NextState = new PlayerWaitState(new PlayerSolutionAcquisitionState());
        IsComplete = true;
        playerController.SendOnPlayerTurnComplete();
    }
    
    public void AnalyseImpactDistance(Vector3 hitLocation) {
        float distance = Vector3.Distance(hitLocation, AIController.Instance.transform.position);
        Debug.Log($"Player payload Distance to AI : {distance}");
        
        if (distance < 2f) {
            NextState = new PlayerWinState();
            IsComplete = true;
            return;
        }
        
        ProgressState();
    }
}
