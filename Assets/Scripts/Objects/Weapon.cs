using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float maxYRotation = 80f; // Limite de rotation maximale sur l'axe Y
    public float minYRotation = -80f; // Limite de rotation minimale sur l'axe Y
    private float currentYRotation = 0f; // Rotation Y actuelle

    // Update is called once per frame
    void Update()
    {
        bool GameIsPaused = PauseMenu.GameIsPaused; // Vérifie si le jeu est en pause

        if (GameIsPaused)
        {
            return; // Sort de la méthode si le jeu est en pause
        }
        else
        {
            // Obtenir la rotation de la caméra
            float mouseY = Input.GetAxis("Mouse Y");
            currentYRotation -= mouseY * 3;
            currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);
        
            // Appliquer la rotation à l'arme
            transform.localRotation = Quaternion.Euler(0f, 90, currentYRotation-2);   
        }
    }

}