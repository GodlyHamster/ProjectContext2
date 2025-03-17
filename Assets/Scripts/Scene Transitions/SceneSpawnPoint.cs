using UnityEngine;

public class SceneSpawnPoint : MonoBehaviour
{
    public string spawnPointID;

    private void Start()
    {
        // Find the transition manager
        if (SceneTransitionManager.Instance != null)
        {
            // Check if this is the correct spawn point
            if (SceneTransitionManager.Instance.lastExitPoint == spawnPointID)
            {
                // Move the player to this spawn point
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = transform.position;
                }
            }
        }
    }
}
