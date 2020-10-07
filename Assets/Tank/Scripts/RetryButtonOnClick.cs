using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Monobehaviour to reload the scene on click
/// </summary>
[RequireComponent(typeof(Button))]
public class RetryButtonOnClick : MonoBehaviour
{

    #region Monobehaviour Methods

    private void OnEnable() {
        GetComponent<Button>().onClick.AddListener(ReloadScene);
    }

    private void OnDisable() {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }


    #endregion

    #region Private Methods

    private void ReloadScene() {
        SceneManager.LoadScene(0);
    }

    #endregion
    
}
