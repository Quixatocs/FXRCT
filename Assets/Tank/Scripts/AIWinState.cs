public class AIWinState : IEntityState {
    private AIController aiController;
    public bool IsComplete { get; }
    public IEntityState NextState { get; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        aiController = controller as AIController;
        aiController.AIWinUI.SetActive(true);
        
    }

    public void OnExit() {
        aiController.AIWinUI.SetActive(false);
    }

    public void ProgressState() {}
}
