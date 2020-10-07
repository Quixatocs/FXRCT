
using UnityEngine;
using UnityEngine.UI;

public class PlayerSolutionAcquisitionState : IEntityState {
    
    private PlayerController playerController;
    
    public bool IsComplete { get; private set; }
    public IEntityState NextState { get; private set; }
    public void OnEnter(StateMachine controller) {
        playerController = controller as PlayerController;
        playerController.PlayerControlsUI.SetActive(true);
        
        playerController.AngleInputField.onValueChanged.AddListener(SetAngle);
        playerController.StrengthInputField.onValueChanged.AddListener(SetStrength);
        playerController.LaunchButton.onClick.AddListener(ProgressState);
        
        playerController.SendOnPlayerUIMessageUpdated("Acquiring Firing Solution");
    }

    public void OnExit() {
        playerController.AngleInputField.onValueChanged.RemoveAllListeners();
        playerController.StrengthInputField.onValueChanged.RemoveAllListeners();
        playerController.LaunchButton.onClick.RemoveAllListeners();
        
        playerController.PlayerControlsUI.SetActive(false);
    }

    public void ProgressState() {
        NextState = new PlayerFireState();
        IsComplete = true;
    }

    private void SetAngle(string newAngle) {
        playerController.LaunchAngle = float.Parse(newAngle);
    }
    
    private void SetStrength(string newStrength) {
        playerController.LaunchStrength = float.Parse(newStrength);
    }
    
    
}
