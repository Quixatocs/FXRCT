﻿
using UnityEngine;

public class AIFireState : IEntityState
{
    private AIController aiController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;

        EventManager.onPayloadCollision += SetImpactDistance;
        
        aiController.SendOnAIUIMessageUpdated("Payload Launching");
        aiController.LaunchPayload();
    }

    public void OnExit() {
        EventManager.onPayloadCollision -= SetImpactDistance;
        
        aiController.SendOnAITurnComplete();
    }

    public void ProgressState() {
        NextState = new AIWaitState();
        IsComplete = true;
    }

    public void SetImpactDistance(Vector3 hitLocation) {
        float distance = Vector3.Distance(hitLocation, PlayerController.Instance.transform.position);
        Debug.Log($"AI payload Distance to Player : {distance}");
        aiController.LaunchHitToTargetDistances.Add(distance);
        ProgressState();
    }
}
