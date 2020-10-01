
public class AIFireState : IEntityState
{
    public bool IsComplete { get; }
    public IEntityState NextState { get; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        AIController aiController = controller as AIController;
        
        aiController.SendOnAIUIMessageUpdated("Payload Launching");
        
        aiController.LaunchPayload();
    }

    public void OnExit() {}

    public void ProgressState() {
        throw new System.NotImplementedException();
    }
}
