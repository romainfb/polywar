using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RandomPatrol : MonoBehaviour
{
    [SerializeField] private float patrolTime = 3f; // Temps de patrouille par défaut
    private NavMeshAgent agent; // Composant NavMeshAgent du PNJ
    private float timer; // Compteur de temps de patrouille

    [SerializeField] private Animator animator; // Composant Animator du PNJ
    [SerializeField] private Transform target; // Cible à poursuivre
    [SerializeField] private float fieldOfViewAngle = 110f; // Angle de champ de vision du PNJ
    [SerializeField] private float viewDistance = 15f; // Distance de vision du PNJ
    [SerializeField] private GameObject projectilePrefab; // Préfabriqué du projectile
    [SerializeField] private Transform muzzleTransform; // Point d'origine du projectile
    [SerializeField] private float shootCooldown = 2f; // Temps de recharge entre chaque tir
    private float shootTimer; // Compteur de temps entre chaque tir
    [SerializeField] private float pursuitProbability = 0.5f; // Probabilité de poursuite de la cible
    private float shootValue = 0f; // Valeur de tir utilisée pour l'animation

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Récupère le composant NavMeshAgent
        timer = patrolTime; // Initialise le compteur de temps de patrouille
        shootTimer = shootCooldown; // Initialise le compteur de temps entre chaque tir
    }

    void Update()
    {
        timer += Time.deltaTime; // Met à jour le compteur de temps de patrouille
        shootTimer += Time.deltaTime; // Met à jour le compteur de temps entre chaque tir

        if (CanSeeTarget())
        {
            OrientTowards(target.position); // Oriente le PNJ vers la cible

            float randomChoice = Random.value;
            if(randomChoice <= pursuitProbability)
            {
                PursueTarget(); // Poursuit la cible
            }
            else
            {
                StopAndShoot(); // Arrête et tire sur la cible
            }
        }
        else if (timer >= patrolTime)
        {
            PatrolRandomly(); // Patrouille aléatoirement
            timer = 0; // Réinitialise le compteur de temps de patrouille
        }

        UpdateAnimation(); // Met à jour l'animation du PNJ
    }
    
    /** 
     * Définit la cible du PNJ.
     * @param target La cible à poursuivre
     */
    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    /** 
     * Poursuit la cible.
     */
    private void PursueTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
        shootValue = 0f; // Réinitialise shootValue si nécessaire
        if (shootTimer >= shootCooldown)
        {
            Fire(); // Tire sur la cible
            shootTimer = 0; // Réinitialise le compteur de temps entre chaque tir
        }
    }

    /** 
     * Arrête et tire sur la cible.
     */
    private void StopAndShoot()
    {
        agent.isStopped = true; // Arrête le PNJ
        if (shootTimer >= shootCooldown)
        {
            Fire(); // Tire sur la cible
            shootTimer = 0; // Réinitialise le compteur de temps entre chaque tir
        }
    }

    /** 
     * Vérifie si le PNJ peut voir la cible.
     * @return bool Retourne vrai si le PNJ peut voir la cible, sinon faux
     */
    private bool CanSeeTarget()
    {
        if (target == null) return false;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToTarget) < fieldOfViewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= viewDistance)
            {
                if (!Physics.Linecast(transform.position, target.position, out RaycastHit hit) || hit.transform == target)
                {
                    return true; // Le PNJ peut voir la cible
                }
            }
        }
        return false; // Le PNJ ne peut pas voir la cible
    }

    /** 
     * Patrouille aléatoirement.
     */
    private void PatrolRandomly()
    {
        agent.isStopped = false;
        Vector3 randomDirection = Random.insideUnitSphere * patrolTime;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, patrolTime, -1);
        agent.SetDestination(navHit.position); // Définit une destination de patrouille aléatoire
        shootValue = 0f; // Réinitialise shootValue pour la patrouille
    }

    /** 
     * Tire sur la cible.
     */
    private void Fire()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, muzzleTransform.position, Quaternion.identity);
        projectileInstance.transform.LookAt(target.position); // Oriente le projectile vers la cible
        shootValue = 1f; // Définit shootValue à 1 immédiatement après le tir
        StartCoroutine(ResetShootValueAfterDelay()); // Réinitialise shootValue après un délai
    }

    /** 
     * Réinitialise shootValue après un délai.
     */
    private IEnumerator ResetShootValueAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Délai avant la réinitialisation de shootValue
        shootValue = 0f; // Réinitialise shootValue
    }

    /** 
     * Met à jour l'animation du PNJ.
     */
    private void UpdateAnimation()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.magnitude;

        animator.SetFloat("x", localVelocity.x); // Met à jour la vitesse de déplacement horizontale dans l'animation
        animator.SetFloat("z", localVelocity.z); // Met à jour la vitesse de déplacement verticale dans l'animation
        animator.SetFloat("speed", speed); // Met à jour la vitesse de déplacement totale dans l'animation
        animator.SetFloat("shootValue", shootValue); // Met à jour la valeur de tir dans l'animation
    }

    /** 
     * Oriente le PNJ vers une position cible.
     * @param targetPosition La position cible à orienter vers
     */
    private void OrientTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // Garde le PNJ orienté horizontalement
        transform.rotation = Quaternion.LookRotation(direction); // Oriente le PNJ vers la cible
    }
}
