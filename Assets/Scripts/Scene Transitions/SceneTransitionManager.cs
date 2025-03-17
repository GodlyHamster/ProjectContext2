using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Header("Player Spawn Data")]

    public string lastExitPoint; // Identifier for where the player exited

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[SceneTransitionManager] Initialized and persisting between scenes.");
        }
        else
        {
            Debug.LogWarning("[SceneTransitionManager] Duplicate instance detected, destroying extra.");
            Destroy(gameObject);
        }
    }

}
