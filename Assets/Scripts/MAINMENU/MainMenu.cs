using UnityEngine;

public class MainMenu : MonoBehaviour
{
   public GameObject mainMenuUI;
   public GameObject settingsMenuUI;

    public void OpenSettingsMenu()
    {
         mainMenuUI.SetActive(false);
         settingsMenuUI.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
         settingsMenuUI.SetActive(false);
         mainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void StartGame()
    {
        // Load the main game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
    }
}
