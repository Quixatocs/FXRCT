
using UnityEngine;

public class AIMovementState : IEntityState {
    private AIController aiController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;

        Bounds bounds = aiController.GroundCollider.bounds;
        float newXposition = Random.Range(bounds.min.x, bounds.max.x);
        float newYposition = Random.Range(bounds.min.y, bounds.max.y);
        float newZposition = Random.Range(bounds.min.z, bounds.max.z);
        
        Vector3 newTarget = new Vector3(newXposition, newYposition, newZposition);

        aiController.TargetMovementLocation = newTarget;
        aiController.SendOnAIUIMessageUpdated("Moving to Location");
    }

    public void OnExit() {}

    public void ProgressState() {
        NextState = new AIWaitState();
        IsComplete = true;
        aiController.SendOnAITurnComplete();
    }
        
    
    
}
