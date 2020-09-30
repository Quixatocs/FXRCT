using System;

public class AIController : StateMachine
{
    
    #region Singleton Instance

    private static AIController instance;

    #endregion
    
    #region Monobehaviour Methods

    private void Awake() {
        // Singleton implementation
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        //TODO transition to the opening state
    }
    
    #endregion
}
