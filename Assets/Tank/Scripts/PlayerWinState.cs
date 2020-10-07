using UnityEngine;

public class PlayerWinState : IEntityState {
    private PlayerController playerController;
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
}
