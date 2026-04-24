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
    public AudioClip godMusic;
    public AudioClip devilMusic;

    private bool isInMenu = false;
    private AudioClip currentMenuClip;

    public GameObject canvaMusic;
    public GameObject canvasScene;
    public GameObject configButton;
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

        canvasScene = GameObject.Find("Canvas");
        configButton = GameObject.Find("ConfigButton");

        configButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(MusicMenu);

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
    }

    public void MusicMenu()
    {
        canvaMusic.SetActive(!canvaMusic.activeSelf);
        canvasScene.SetActive(!canvasScene.activeSelf);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CambiarMusica(scene.name);
        canvasScene = GameObject.Find("Canvas");
        configButton = GameObject.Find("ConfigButton");

        if (scene.name == "Ciudad")
        {
          canvasScene.SetActive(false);  
        }
        configButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(MusicMenu);

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
        else  if (sceneName == "Ciudad")
        {
            if (GameManager.mood == false)
            {
                musicSource.clip = godMusic;
            }
            else
            {
                musicSource.clip = devilMusic;
            }  
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