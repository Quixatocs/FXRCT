using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerUIMessageDisplay : MonoBehaviour {
    
    private Text text;
    
    private void Awake() {
        text = GetComponent<Text>();
    }

    private void OnEnable() {
        PlayerController.onPlayerUIMessageUpdated += UpdateText;
    }
    
    private void OnDisable() {
        PlayerController.onPlayerUIMessageUpdated -= UpdateText;
    }

    private void UpdateText(string newMessage) {
        text.text = $"Player : {newMessage}";
    }
}