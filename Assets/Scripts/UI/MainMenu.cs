using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**
     * Charge la scène du jeu.
     * @return void
     */
    public void PlayGame()
    {
        SceneManager.LoadScene("Cinematic");
    }

    /**
     * Quitte l'application.
     * @return void
     */
    public void QuitGame()
    {
        Application.Quit();
    }
}