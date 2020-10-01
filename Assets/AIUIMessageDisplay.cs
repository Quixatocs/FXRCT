using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AIUIMessageDisplay : MonoBehaviour {
    
    private Text text;
    
    private void Awake() {
        text = GetComponent<Text>();
    }

    private void OnEnable() {
        AIController.onAIUIMessageUpdated += UpdateText;
    }
    
    private void OnDisable() {
        AIController.onAIUIMessageUpdated -= UpdateText;
    }

    private void UpdateText(string newMessage) {
        text.text = $"AI : {newMessage}";
    }
}
