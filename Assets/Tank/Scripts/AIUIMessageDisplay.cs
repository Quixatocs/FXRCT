using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Monobehaviour to be attached to text components that will receive
/// and display UI information sent through an event 
/// </summary>
[RequireComponent(typeof(Text))]
public class AIUIMessageDisplay : MonoBehaviour {

    #region Fields

    private Text text;

    #endregion

    #region Monobehaviour Methods

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void OnEnable() {
        AIController.onAIUIMessageUpdated += UpdateText;
    }
    
    private void OnDisable() {
        AIController.onAIUIMessageUpdated -= UpdateText;
    }

    #endregion

    #region Private Methods

    private void UpdateText(string newMessage) {
        text.text = $"AI : {newMessage}";
    }

    #endregion
}
