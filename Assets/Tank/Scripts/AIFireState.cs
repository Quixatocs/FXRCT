
public class AIFireState : IEntityState
{
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        AIController aiController = controller as AIController;
        
        aiController.SendOnAIUIMessageUpdated("Payload Launching");
        
        aiController.LaunchPayload();
    }

    public void OnExit() {}

    public void ProgressState() {
        NextState = new AIWaitState();
        IsComplete = true;
    }
}
