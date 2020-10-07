/// <summary>
/// Class representing a Win for the AI
/// </summary>
public class AIWinState : IEntityState {

    #region Fields

    private AIController aiController;

    #endregion
    
    #region IEntityState Implementation
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
    
    #endregion
}
