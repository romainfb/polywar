using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private AudioSource HelicoSound; // Source audio pour le son de l'hélicoptère
    public float speed = 5f; // Vitesse de déplacement de l'hélicoptère
    public GameObject propelers; // Référence aux hélices avant
    public GameObject backPropelers; // Référence aux hélices arrière

    // Start is called before the first frame update
    void Start()
    {
        // Détruire l'hélicoptère après un délai
        Invoke("DestroyHelicopter", 16f);
        // Démarrer le son de l'hélicoptère
        HelicoSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Avancer l'hélicoptère dans sa direction vers l'avant
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        // Faire tourner les hélices avant
        propelers.transform.Rotate(0, 0, 1000 * Time.deltaTime);
        // Faire tourner les hélices arrière
        backPropelers.transform.Rotate(1000, 0, 0 * Time.deltaTime);
    }

    /**
     * Détruit l'objet hélicoptère et arrête le son.
     * @return void
     */
    void DestroyHelicopter()
    {
        // Détruire l'hélicoptère
        Destroy(gameObject);
        // Arrêter le son de l'hélicoptère
        HelicoSound.Stop();
    }
}