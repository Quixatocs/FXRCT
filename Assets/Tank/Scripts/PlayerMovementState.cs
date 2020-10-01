public class PlayerMovementState : IEntityState
{
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        PlayerController playerController = controller as PlayerController;

        playerController.TargetLocation = playerController.SelectedTargetLocation;
        playerController.SendOnPlayerUIMessageUpdated("Moving to Location");
    }

    public void OnExit() {}

    public void ProgressState() {
        NextState = new PlayerWaitState();
        IsComplete = true;
    }
        
    
    
}