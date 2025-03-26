using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField]
    private StudioEventEmitter gameMusic;
    [SerializeField]
    private StudioEventEmitter churchMusic;

    [SerializeField]
    private float musicVolume = 1f;

    private int festivalProgression;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameMusic.EventInstance.setVolume(musicVolume);
        churchMusic.EventInstance.setVolume(musicVolume);
    }

    [ContextMenu("Enter church")]
    public void EnterChurch()
    {
        gameMusic.Stop();
        churchMusic.Play();
    }

    [ContextMenu("Exit church")]
    public void ExitChurch()
    {
        churchMusic.Stop();
        gameMusic.Play();
    }

    /// <summary>
    /// sets the progression for the festival music. 0 is none, 100 is completed
    /// </summary>
    /// <param name="progression"></param>
    public void SetFestivalProgress(float progression)
    {
        gameMusic.SetParameter("progression", progression);
    }
}
