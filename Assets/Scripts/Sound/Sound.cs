using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sound
{
    public class Sound : MonoBehaviour
    {
        // AudioSource 0 for music
        // AudioSource 1 for sfx

        public AudioClip UIBubbleSound;
        public AudioClip UICloseSound;

        public AudioClip[] menuTracks;

        public AudioClip[] gameTracks;

        public bool loopSequence;


        [HideInInspector]
        public AudioSource[] audioSources;

        public static Sound Instance;

        private Coroutine currentTrackCoroutine;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
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
                //  ����� ��� ����
                currentTrackCoroutine = StartCoroutine(PlayTracksSequentially(menuTracks));
            }
            else
            {
                //  ����� ��� ����
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
        public void PlayCloseSound()
        {
            audioSources[1].clip = UICloseSound;
            audioSources[1].Play();
        }
        public void PlaySound(AudioClip clip)
        {
            if (clip != null && audioSources != null)
            {
                audioSources[1].clip = clip;
                audioSources[1].Play();
            }
            else
            {
                Debug.LogWarning("Аудиоклип нет или нет AudioSource");
            }
        }
    }
}
