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
            // Store where the player is leaving from
            SceneTransitionManager.Instance.lastExitPoint = exitPointID;

            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
