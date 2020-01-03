using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Image pauseMenuPanel = null;

    [SerializeField]
    Image gameOverPanel = null;

    [SerializeField]
    EventSystem evtSystem = null;

    [SerializeField]
    Button resumeButton = null;

    [SerializeField]
    Button retryButton = null;

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
        if (!pauseMenuPanel || !evtSystem || !resumeButton)
            return;

        Time.timeScale = 0f;
        pauseMenuPanel.gameObject.SetActive(true);
        evtSystem.SetSelectedGameObject(resumeButton.gameObject);
    }

    public void ResumeGame()
    {
        if (!pauseMenuPanel || !evtSystem)
            return;

        Time.timeScale = 1f;
        pauseMenuPanel.gameObject.SetActive(false);
        evtSystem.SetSelectedGameObject(null);
    }

    public void GameOver()
    {
        if (!gameOverPanel || !evtSystem || !retryButton)
            return;

        gameOverPanel.gameObject.SetActive(true);
        evtSystem.SetSelectedGameObject(retryButton.gameObject);
    }

    public void Retry()
    {
        if (!gameOverPanel || !evtSystem)
            return;

        gameOverPanel.gameObject.SetActive(false);
        evtSystem.SetSelectedGameObject(null);
    }
}
