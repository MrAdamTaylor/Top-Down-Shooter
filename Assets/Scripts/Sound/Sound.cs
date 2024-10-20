using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sound : MonoBehaviour
{
    // AudioSource 0 for music
    // AudioSource 1 for sfx

    public AudioClip UIBubbleSound;

    public AudioClip[] menuTracks;

    public AudioClip[] gameTracks;

    public bool loopSequence;

    [HideInInspector]
    public AudioSource[] audioSources;

    public static Sound instance;

    private Coroutine currentTrackCoroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSources = GetComponents<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;
        PlayTrackForScene(sceneIndex); 
    }

    void Start()
    {
        PlayTrackForScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayTrackForScene(int sceneIndex)
    {
        if (currentTrackCoroutine != null)
        {
            StopCoroutine(currentTrackCoroutine);  
        }
       
        if (sceneIndex == 0 )
        {
            //  треки для меню
            currentTrackCoroutine = StartCoroutine(PlayTracksSequentially(menuTracks));
        }
        else
        {
            //  треки для игры
            currentTrackCoroutine = StartCoroutine(PlayTracksSequentially(gameTracks));
        }
    }

    IEnumerator PlayTracksSequentially(AudioClip[] tracks)
    {
        int trackIndex = Random.Range(0, tracks.Length);

        while (true)
        {
            audioSources[0].clip = tracks[trackIndex];
            audioSources[0].Play();
            yield return new WaitForSeconds(audioSources[0].clip.length);

           
            trackIndex = Random.Range(0, tracks.Length);

            if (!loopSequence)
            {
                break; 
            }
        }
    }

    public void PlayBubbleSound()
    {
        audioSources[1].clip = UIBubbleSound;
        audioSources[1].Play();
    }
}
