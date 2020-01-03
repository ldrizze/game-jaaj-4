using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Image pauseMenuPanel = null;

    [SerializeField]
    EventSystem evtSystem = null;

    [SerializeField]
    Button resumeButton = null;

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1f) // Game not paused
            {
                PauseGame();
            }
            else // Game paused
            {
                ResumeGame();
            }
        }
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenuPanel)
            pauseMenuPanel.gameObject.SetActive(true);

        if (evtSystem && resumeButton)
            evtSystem.SetSelectedGameObject(resumeButton.gameObject);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pauseMenuPanel)
            pauseMenuPanel.gameObject.SetActive(false);

        if (evtSystem)
            evtSystem.SetSelectedGameObject(null);
    }
}
