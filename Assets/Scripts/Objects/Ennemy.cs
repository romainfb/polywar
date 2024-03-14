using UnityEngine;

public class Ennemy : MonoBehaviour
{
    private GameData GameData; // Référence vers l'objet GameData pour suivre le nombre de kills.

    /**
     * Méthode appelée au démarrage de l'objet pour initialiser la référence GameData.
     */
    private void Start()
    {
        // Trouver l'objet GameData dans la scène
        GameData = GameObject.FindObjectOfType<GameData>();
    }

    /**
     * Méthode appelée lorsqu'une collision se produit avec cet ennemi.
     * Si la collision concerne un projectile, augmente le nombre de kills dans l'objet GameData et détruit cet ennemi.
     * @param collision L'objet de collision avec lequel cet ennemi entre en collision.
     * @return void
     */
    private void OnCollisionEnter(Collision collision)
    {
        // Vérifier si l'objet en collision est un Projectile
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            // Augmenter le nombre de kills dans l'objet GameData
            GameData.AddKill();
            // Détruire l'ennemi
            Destroy(gameObject);
        }
    }
}