
using UnityEngine;

public class PlayerFireState : IEntityState
{
    private PlayerController playerController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;

        EventManager.onPayloadCollision += SetImpactDistance;
        
        playerController.SendOnPlayerUIMessageUpdated("Payload Launching");
        playerController.LaunchPayload();
    }

    public void OnExit() {
        EventManager.onPayloadCollision -= SetImpactDistance;
        
        playerController.SendOnPlayerTurnComplete();
    }

    public void ProgressState() {
        NextState = new PlayerWaitState();
        IsComplete = true;
    }
    
    public void SetImpactDistance(Vector3 hitLocation) {
        float distance = Vector3.Distance(hitLocation, AIController.Instance.transform.position);
        Debug.Log($"Player payload Distance to AI : {distance}");
        ProgressState();
    }
}
