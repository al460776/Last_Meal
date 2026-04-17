using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio SFX -----------")]
    public AudioClip click; //Para cuando suene por ejemplo la muerte o cualquier otra cosa usar esto
    [Header("----------- Background Music -----------")]
    public AudioClip musicMenu1;
    public AudioClip musicMenu2;
    public AudioClip musicNivel;

    private bool isInMenu = false;
    private AudioClip currentMenuClip;

    public GameObject canvaMusic;
    private void Awake()
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
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CambiarMusica(SceneManager.GetActiveScene().name);
        canvaMusic.SetActive(false);
    }

    private void Update()
    {
        if (isInMenu && !musicSource.isPlaying)
        {
            AlternarMusicaMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvaMusic.SetActive(!canvaMusic.activeSelf);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CambiarMusica(scene.name);
    }

    private void CambiarMusica(string sceneName)
    {
        if (sceneName == "MenuPrincipal")
        {
            isInMenu = true;
            // Elegir aleatoriamente entre las dos canciones del menú
            currentMenuClip = Random.Range(0, 2) == 0 ? musicMenu1 : musicMenu2;
            musicSource.clip = currentMenuClip;
            musicSource.Play();
        }
        else if (sceneName == "Ciudad")
        {
            isInMenu = false;
            musicSource.clip = musicNivel;
            musicSource.Play();
        }
        else
        {
            isInMenu = false;
            musicSource.Stop();
        }
    }

    private void AlternarMusicaMenu()
    {
        // Cambia a la otra canción
        currentMenuClip = (currentMenuClip == musicMenu1) ? musicMenu2 : musicMenu1;
        musicSource.clip = currentMenuClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}