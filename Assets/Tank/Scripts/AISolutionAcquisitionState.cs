﻿using System.Collections;
using UnityEngine;

/// <summary>
/// A class representing the Solution Acquisition state for the AI which
/// calculates and assigns the next firing angle for the AI.
///
/// The angle is calculated based on the distance of the last hit location to the player
/// If it was closer than the previous distance it will use this as the jumping point for the next angle guess
/// </summary>
public class AISolutionAcquisitionState : IEntityState
{
    #region Fields

    private Coroutine enterStateCoroutine;
    private AIController aiController;

    private const float RANDOM_ANGLE_RANGE = 10f;

    readonly WaitForSeconds drammaticWait = new WaitForSeconds(0.6f);

    #endregion

    #region IEntityState Implementation
    
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;
        
        aiController.SendOnAIUIMessageUpdated("Acquiring Firing Solution");
        
        enterStateCoroutine = aiController.StartCoroutine(TakeTurn());
    }

    public void OnExit() {
        if (enterStateCoroutine != null) {
            aiController.StopCoroutine(enterStateCoroutine);
            enterStateCoroutine = null;
        }
    }

    public void ProgressState() {
        NextState = new AIFireState();
        IsComplete = true;
    }
    
    #endregion

    #region Private Methods
    
    /// <summary>
    /// Calculates the angle for the next shot after two hardcoded attempts have been made
    /// Waits inbetween each part of the calculation for drammatic ingame effect
    /// </summary>
    private IEnumerator TakeTurn() {
        yield return drammaticWait;
        aiController.SendOnAIUIMessageUpdated("Calculating Launch Angle...");

        float newAngleCalculation;
        if (aiController.LaunchAngles.Count == 0) {
            // This is our first try so default to 60 degrees
            newAngleCalculation = 60f;
            aiController.LaunchAngles.Add(newAngleCalculation);
        }
        else if (aiController.LaunchAngles.Count == 1) {
            // This is our second try, we need a second datapoint to draw any calculation
            // so this time try  degrees
            newAngleCalculation = aiController.LaunchAngles[aiController.LaunchAngles.Count - 1] + 
                                  Random.Range(-RANDOM_ANGLE_RANGE, RANDOM_ANGLE_RANGE);
            aiController.LaunchAngles.Add(newAngleCalculation);
        }
        else {
            int count = aiController.LaunchHitToTargetDistances.Count;
            // Now on our third try we have enough data to decide which point we should center guesses around
            // So first check if we got closer or futher away on our second shot
            if (aiController.LaunchHitToTargetDistances[count - 1] < aiController.LaunchHitToTargetDistances[count - 2]) {
                // if we're closer then we go around the last value we used
                newAngleCalculation = aiController.LaunchAngles[aiController.LaunchAngles.Count - 1] + 
                                      Random.Range(-RANDOM_ANGLE_RANGE, RANDOM_ANGLE_RANGE);
                
            }
            else {
                // if we're further away then we go around the last but one value we used
                newAngleCalculation = aiController.LaunchAngles[aiController.LaunchAngles.Count - 2] + 
                                      Random.Range(-RANDOM_ANGLE_RANGE, RANDOM_ANGLE_RANGE);
            }
            aiController.LaunchAngles.Add(newAngleCalculation);
        }
        
        yield return drammaticWait;
        aiController.SendOnAIUIMessageUpdated("Setting Launch Strength...");
        
        // As long as a strength value allows us to fire past the target then adjusting 
        // the angle is all we need to do to be able to eventually hit the target
        aiController.LaunchStrengths.Add(30f);
        
        yield return drammaticWait;
        aiController.SendOnAIUIMessageUpdated("Solution Acquired!");
        
        yield return drammaticWait;
        
        ProgressState();
    }
    #endregion
}
