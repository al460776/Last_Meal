using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public GameObject button;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(button);
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Tiene que salir");
    }

    public void Ciudad()
    {
        SceneManager.LoadScene("Ciudad");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
