using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMuzzle : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Le préfabriqué du projectile à tirer
    [SerializeField] private GameObject fireShot; // L'effet visuel du tir
    [SerializeField] private AudioSource ShotSound; // Le son du tir

    // Update is called once per frame
    void Update()
    {
        // Vérifie si le joueur appuie sur le bouton de tir
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireProjectile(); // Tire un projectile
        }
        
        UpdateFireShotVisibility(); // Met à jour la visibilité de l'effet visuel du tir
    }

    /**
     * Tire un projectile dans la direction vers laquelle le joueur regarde.
     */
    void FireProjectile()
    {
        // Obtenir la direction dans laquelle le joueur regarde
        Vector3 direction = transform.forward;
        ShotSound.Play(); // Jouer le son du tir
        
        // Instancier le projectile avec la direction appropriée
        var instance = Instantiate(projectilePrefab, transform.position, transform.rotation);
        instance.GetComponent<Projectile>().SetDirection(direction);
    }
    
    /**
     * Met à jour la visibilité de l'effet visuel du tir en fonction de si le joueur tire ou non.
     */
    void UpdateFireShotVisibility()
    {
        // Activer ou désactiver l'objet fireShot en fonction de si le joueur appuie ou non sur le bouton de tir
        fireShot.SetActive(Input.GetKey(KeyCode.Mouse0));
    }
}