using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Indique si le jeu est en pause
    public static bool isOptionMenu = false; // Indique si le menu des options est ouvert
    [SerializeField] private GameObject PauseMenuUI; // Référence au menu de pause
    [SerializeField] private GameObject OptionMenuUI; // Référence au menu des options

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                if (isOptionMenu)
                {
                    // Inverse l'état d'affichage du menu des options
                    OptionMenuUI.SetActive(!OptionMenuUI.activeSelf);
                }
                Debug.Log("Resume");
                Resume(); // Reprendre le jeu
            }
            else if(!isOptionMenu)
            {
                Pause(); // Mettre le jeu en pause
            }
        }        
    }
    
    /**
     * Reprendre le jeu en désactivant le menu de pause.
     * @return void
     */
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    /**
     * Mettre le jeu en pause en activant le menu de pause.
     * @return void
     */
    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    
    /**
     * Quitter l'application.
     * @return void
     */
    public void QuitGame()
    {
        Application.Quit();
    }
    
    /**
     * Afficher le menu des options.
     * @return void
     */
    public void ShowOptionMenu()
    {
        // Inverser l'état d'affichage du menu de pause
        PauseMenuUI.SetActive(!PauseMenuUI.activeSelf);
        // Inverser l'état d'affichage du menu des options
        OptionMenuUI.SetActive(!OptionMenuUI.activeSelf);
        // Mettre à jour l'état du menu des options
        isOptionMenu = PauseMenuUI.activeSelf;
        // Mettre le jeu en pause si le menu de pause est actif
        GameIsPaused = PauseMenuUI.activeSelf;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
