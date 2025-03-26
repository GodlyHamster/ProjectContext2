using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField]
    private StudioEventEmitter audioEmitter;

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
        audioEmitter.EventInstance.setVolume(musicVolume);
    }

    /// <summary>
    /// sets the progression for the festival music. 0 is none, 100 is completed
    /// </summary>
    /// <param name="progression"></param>
    public void SetFestivalProgress(float progression)
    {
        audioEmitter.SetParameter("progression", progression);
    }
}
