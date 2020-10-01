
public class PlayerLocationSelectState : IEntityState
{
    
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        PlayerController playerController = controller as PlayerController;
        
        playerController.SendOnPlayerUIMessageUpdated("Selecting Location");
    }

    public void OnExit() {}

    public void ProgressState() {
        NextState = new PlayerMovementState();
        IsComplete = true;
    }
}
