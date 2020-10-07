public class PlayerMovementState : IEntityState {
    
    private PlayerController playerController;
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
        
    
    
}