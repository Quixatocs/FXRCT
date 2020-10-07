/// <summary>
/// Class representing the Win state for the player
/// </summary>
public class PlayerWinState : IEntityState {
    
    #region Fields

    private PlayerController playerController;

    #endregion
    
    #region IEntityState Implementation
    public bool IsComplete { get; }
    public IEntityState NextState { get; }
    public void OnEnter(StateMachine controller) {
        if (controller == null) return;

        playerController = controller as PlayerController;
        playerController.PlayerWinUI.SetActive(true);
    }

    public void OnExit() {
        playerController.PlayerWinUI.SetActive(false);
    }

    public void ProgressState() {}
    
    #endregion
}
