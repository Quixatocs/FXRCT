public class PlayerTurnState : IEntityState {
    
    private PlayerController playerController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;
        
        playerController.SendOnPlayerUIMessageUpdated("Taking Turn");
    }

    public void OnExit() {
        playerController.SendOnPlayerTurnComplete();
    }

    public void ProgressState() {
        NextState = new PlayerWaitState();
        IsComplete = true;
    }
}