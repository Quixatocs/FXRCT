public class AITurnState : IEntityState {
    
    private AIController aiController;
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        AIController aiController = controller as AIController;
        
        aiController.SendOnAIUIMessageUpdated("Taking Turn");
    }

    public void OnExit() {
        aiController.SendOnAITurnComplete();
    }

    public void ProgressState() {
        NextState = new AIWaitState();
        IsComplete = true;
    }
}
