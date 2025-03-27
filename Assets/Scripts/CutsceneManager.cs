using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;
    [SerializeField]
    private PlayableAsset playable;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    [ContextMenu("Start Cutscene")]
    public void PlayCutscene()
    {
        if (playable == null)
        {
            Debug.LogWarning("Can't play cutscene because playable is null");
            return;
        }

        if (playableDirector.playableAsset == null)
        {
            playableDirector.playableAsset = playable;
        }
        playableDirector.Play();
    }

    public void PlayCutscene(PlayableAsset cutsceneAsset)
    {
        playable = cutsceneAsset;
        playableDirector.playableAsset = playable;
        playableDirector.Play();
    }

    [ContextMenu("Pause Cutscene")]
    public void PauseCutscene()
    {
        playableDirector.Pause();
    }


    [ContextMenu("End Cutscene")]
    public void StopCutscene()
    {
        playableDirector.Stop();
        playableDirector.playableAsset = null;
    }
}
