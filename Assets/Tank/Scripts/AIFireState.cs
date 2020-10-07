using UnityEngine;

/// <summary>
/// A state representing the AI player firing a payload
/// </summary>
public class AIFireState : IEntityState {

    #region Fields

    private const float HIT_DISTANCE = 2f;
    private AIController aiController;

    #endregion

    #region IEntityState Implementation
    
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
    
    #endregion

    #region Public Methods

    /// <summary>
    /// Analyses the distance from impact to the player and checks whether this is considered a hit
    /// </summary>
    public void AnalyseImpactDistance(Vector3 hitLocation) {
        float distance = Vector3.Distance(hitLocation, PlayerController.Instance.transform.position);
        Debug.Log($"AI payload Distance to Player : {distance}");
        aiController.LaunchHitToTargetDistances.Add(distance);

        if (distance < HIT_DISTANCE) {
            NextState = new AIWinState();
            IsComplete = true;
            return;
        }
        
        ProgressState();
    }
    
    #endregion
}
