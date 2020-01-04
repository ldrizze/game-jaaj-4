using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowHowToPlay()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        Application.Quit(0);
    }
}
