/// <summary>
/// Class representing the movement of the player to its player-chosen location
/// </summary>
public class PlayerMovementState : IEntityState {

    #region Fields

    private PlayerController playerController;

    #endregion
    
    #region IEntityState Implementation
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;

        playerController.TargetLocation = playerController.SelectedTargetLocation;
        playerController.SendOnPlayerUIMessageUpdated("Moving to Location");
    }

    public void OnExit() {
        playerController.SendOnPlayerTurnComplete();
    }

    public void ProgressState() {
        NextState = new PlayerWaitState(new PlayerSolutionAcquisitionState());
        IsComplete = true;
    }
    #endregion
}