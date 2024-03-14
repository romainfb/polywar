using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private float sensitivity = 3f; // Sensibilité de la souris pour le mouvement de la caméra
    [SerializeField] private float jumpHeight = 2.0f; // Hauteur de saut du joueur
    [SerializeField] private float timeToJumpApex = 0.5f; // Temps nécessaire pour atteindre le sommet du saut
    [SerializeField] private float speed = 5f; // Vitesse de déplacement du joueur
    [SerializeField] private float sprintSpeed = 7f; // Vitesse de sprint du joueur
    [SerializeField] private Rigidbody body; // Composant Rigidbody du joueur
    [SerializeField] private GameObject cameraObject; // Caméra du joueur
    [SerializeField] private float maxYRotation = 80f; // Angle maximal de rotation de la caméra vers le haut
    [SerializeField] private float minYRotation = -80f; // Angle minimal de rotation de la caméra vers le bas
    [SerializeField] private float currentYRotation = 0f; // Angle actuel de rotation de la caméra selon l'axe Y
    [SerializeField] private int walkFOV = 57; // Champ de vision lors de la marche
    [SerializeField] private int runFOV = 62; // Champ de vision lors du sprint
    [SerializeField] private int aimFOV = 80; // Champ de vision lors de l'alignement de la visée

    [SerializeField] private GameObject PauseMenuUI; // Menu de pause
    [SerializeField] private GameData GameData; // Données du jeu

    private Camera mainCamera; // Caméra principale
    private float gravity; // Force de gravité appliquée au joueur
    private float currentSpeed; // Vitesse actuelle de déplacement du joueur

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur au centre de l'écran
        Cursor.visible = false; // Masque le curseur
        mainCamera = cameraObject.GetComponent<Camera>(); // Récupère la référence de la caméra

        // Calcul de la gravité en fonction de la hauteur de saut et du temps pour atteindre le sommet du saut
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
    }

    private void FixedUpdate()
    {
        Vector3 movementDirection = new Vector3(0, body.velocity.y, 0); // Direction de déplacement du joueur
        bool sprinting = false; // Indicateur de sprint

        // Gestion des déplacements
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += transform.forward;
            mainCamera.fieldOfView = walkFOV;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementDirection -= transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection -= transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += transform.right;
        }

        // Vérifie si le joueur peut sprinter
        if (GameData.isAbleToUseStamina && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
        }

        // Gestion du sprint
        if (sprinting)
        {
            mainCamera.fieldOfView = runFOV;
            currentSpeed = sprintSpeed;
            GameData.UpdateStamina(0.01f); // Diminue la stamina du joueur pendant le sprint
        }
        else
        {
            mainCamera.fieldOfView = walkFOV;
            currentSpeed = speed;
            GameData.RecoverStamina(); // Régénère la stamina du joueur
        }

        // Gestion de la visée
        if (Input.GetKey(KeyCode.Mouse1))
        {
            mainCamera.fieldOfView = walkFOV;
        }
        else
        {
            mainCamera.fieldOfView = aimFOV;
        }

        movementDirection = movementDirection.normalized; // Normalise la direction de déplacement

        // Applique la vitesse de déplacement et la gravité au joueur
        Vector3 velocity = new Vector3(movementDirection.x * currentSpeed, body.velocity.y, movementDirection.z * currentSpeed);
        body.velocity = velocity;
        body.velocity += Vector3.up * gravity * Time.fixedDeltaTime;

        // Vérifie si le joueur saute
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Update()
    {
        bool GameIsPaused = PauseMenu.GameIsPaused;

        if (GameIsPaused)
        {
            return;
        }
        else
        {
            // Gestion des déplacements
            float horizontalInput = 0f;
            float verticalInput = 0f;

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            moveDirection = transform.TransformDirection(moveDirection);

            body.velocity = moveDirection * speed;

            // Gestion de la rotation de la caméra
            float mouseY = Input.GetAxis("Mouse Y");
            currentYRotation -= mouseY * sensitivity;
            currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);
            cameraObject.transform.localRotation = Quaternion.Euler(currentYRotation, 0f, 0f);

            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseX * sensitivity);
        }

    }

    /**
     * Fait sauter le joueur.
     * @return void
     */
    void Jump()
    {
        // Calcul de la vélocité initiale pour le saut
        float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
        body.velocity += Vector3.up * jumpVelocity;
    }

}
