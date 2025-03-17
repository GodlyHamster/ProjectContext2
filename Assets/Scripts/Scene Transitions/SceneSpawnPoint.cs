using UnityEngine;
using System.Collections;

public class SceneSpawnPoint : MonoBehaviour
{
    public string spawnPointID; // The ID of this spawn point

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // Ensure scene is fully loaded

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Debug.Log($"[SceneSpawnPoint] Moving player to spawn point: {transform.position}");
            player.transform.position = transform.position;

            // Wait a bit and check if the position changes again
            yield return new WaitForSeconds(1f);
            Debug.Log($"[SceneSpawnPoint] One second later, player is at: {player.transform.position}");
        }
    }

}
