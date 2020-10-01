
public class PlayerLocationSelectState : IEntityState
{
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        
        PlayerController.SendOnPlayerUIMessageUpdated("Selecting Location");
    }

    public void OnExit() {}

    public void ProgressState() {
        NextState = new PlayerMovementState();
        IsComplete = true;
    }
}
