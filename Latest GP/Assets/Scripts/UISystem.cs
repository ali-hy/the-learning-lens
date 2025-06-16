using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // References to your different menu panels
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject levelSelectPanel;

    // Keep track of the currently active panel
    private GameObject currentActivePanel;

    private void Start()
    {
        // Set the main menu as the default active panel at start
        currentActivePanel = mainMenuPanel;

        // Make sure only the main menu is active at start
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    // Call this method to switch to a specific panel
    public void SwitchToPanel(GameObject targetPanel)
    {
        // Deactivate the current panel
        if (currentActivePanel != null)
        {
            currentActivePanel.SetActive(false);
        }

        // Activate the target panel
        targetPanel.SetActive(true);

        // Update the current active panel
        currentActivePanel = targetPanel;
    }

    // Convenience methods for switching to specific panels
    // These will be connected to button OnClick events

    public void ShowMainMenu()
    {
        SwitchToPanel(mainMenuPanel);
    }

    public void ShowSettings()
    {
        SwitchToPanel(settingsPanel);
    }

    public void ShowCredits()
    {
        SwitchToPanel(creditsPanel);
    }

    public void ShowLevelSelect()
    {
        SwitchToPanel(levelSelectPanel);
    }
    public void SwitchToBikeScene()
    {
        LoadScene("SampleScene");
    }
    public void SwitchToArScene()
    {
        LoadScene("ArScene");
    }
    public void SwitchToMainScene()
    {
        LoadScene("MainMenu");
    }
    // Load a scene by its name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
