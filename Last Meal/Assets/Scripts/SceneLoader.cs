using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
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
