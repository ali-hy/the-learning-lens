using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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
        if (mainMenuPanel)
            mainMenuPanel.SetActive(true);

        if (settingsPanel)
            settingsPanel.SetActive(false);

        if (creditsPanel)
            creditsPanel.SetActive(false);

        if (levelSelectPanel)
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

    public void SwitchToArScene()
    {
        SceneManager.LoadScene("ArScene");
    }
    
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // ------- Handle button clicks -------
    // This method is called when a lesson item is clicked in the level select panel
    public void SwitchToTrainingScene(LessonItem lessonItem)
    {
        LessonSceneLoader.LessonItem = lessonItem;
        SceneManager.LoadScene("LessonScene");
    }

    // Handle login button click
    public void OnLoginBtnClicked()
    {
        GameObject form = GameObject.Find("Login Form");

        if (form != null)
            throw new System.Exception("Login Form not found");

        var EmailField = form.transform.Find("Email Field").GetComponent<TextMeshProUGUI>();
        var PasswordField = form.transform.Find("Password Field").GetComponent<TextMeshProUGUI>();

        StartCoroutine(Api.Login(EmailField.text, PasswordField.text, i => Debug.Log($"Logged in with {i.AccessToken}")));
    }
}
