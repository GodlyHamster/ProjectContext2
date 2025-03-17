using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTrigger : MonoBehaviour
{
    [Header("Transition Settings")]
    public string sceneToLoad; // Scene name to load
    public string exitPointID; // Unique ID of this exit (to be remembered)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[TransitionTrigger] Setting lastExitPoint: {exitPointID} before loading {sceneToLoad}");
            SceneTransitionManager.Instance.lastExitPoint = exitPointID;

            // Ensure SceneTransitionManager persists
            if (SceneTransitionManager.Instance == null)
            {
                Debug.LogError("[TransitionTrigger] SceneTransitionManager instance is missing!");
            }

            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
