using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private string targetSceneName = "MainMenu";

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPanel()
    {
        panelToShow.SetActive(true);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}