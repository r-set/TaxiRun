using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Home");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
