using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f; // Vitesse du projectile
    [SerializeField] private Vector3 direction = new Vector3(0, 0, 1); // Direction du projectile
    [SerializeField] private float lifeTime = 1; // Durée de vie du projectile
    [SerializeField] private Rigidbody body; // Composant Rigidbody du projectile
    [SerializeField] private GameObject projectileImpactPrefab; // Préfabriqué de l'effet d'impact du projectile
    
    private float currentLifeTime; // Temps écoulé depuis la création du projectile
    
    private void Start()
    {
        body.velocity = direction * speed; // Initialise la vélocité du projectile
    }

    private void Update()
    {
        currentLifeTime += Time.deltaTime; // Met à jour le temps de vie du projectile
        // Détruit le projectile s'il a atteint sa durée de vie
        if(currentLifeTime > lifeTime)
        {
            Destroy(gameObject);
        }
    }
    
    /**
     * Définit la direction du projectile.
     * @param newDirection La nouvelle direction du projectile
     * @return void
     */
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Instancie l'effet d'impact
        GameObject impact = Instantiate(projectileImpactPrefab, transform.position, Quaternion.identity);

        // Détruit l'effet d'impact après 3 secondes
        Destroy(impact, 3f);

        // Détruit le projectile
        Destroy(gameObject);
    }  
}