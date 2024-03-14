using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Terrain terrain; // Le terrain où les ennemis seront générés
    [SerializeField] private GameObject enemyPrefab; // Le préfabriqué de l'ennemi à générer
    [SerializeField] private int maxEnemies = 25; // Le nombre maximum d'ennemis à générer
    [SerializeField] private float spawnInterval = 2f; // L'intervalle de temps entre chaque génération d'ennemi
    [SerializeField] private Transform player; // La position du joueur

    private void Start()
    {
        // Démarrez une coroutine pour générer les ennemis de manière aléatoire.
        StartCoroutine(SpawnEnemies());
    }

    /** 
     * Coroutine pour générer les ennemis de manière aléatoire.
     */
    private IEnumerator SpawnEnemies()
    {
        int enemyCount = 0; // Le compteur d'ennemis générés

        while (enemyCount < maxEnemies)
        {
            // Générez une position aléatoire sur la carte.
            Vector3 spawnPosition = GetRandomPositionOnTerrain();

            // Instancier l'ennemi à cette position.
            var instance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            instance.GetComponent<RandomPatrol>().SetTarget(player);

            // Augmenter le compteur d'ennemis.
            enemyCount++;

            // Attendre avant de spawner le prochain ennemi.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    /** 
     * Génère une position aléatoire sur le terrain.
     * @return Vector3 La position aléatoire générée
     */
    private Vector3 GetRandomPositionOnTerrain()
    {
        // Obtenez les limites du terrain.
        Vector3 terrainSize = terrain.terrainData.size;
        float terrainX = terrainSize.x / 2f;
        float terrainZ = terrainSize.z / 2f;

        // Générer une position aléatoire sur le terrain.
        float randomX = UnityEngine.Random.Range(-terrainX, terrainX);
        float randomZ = UnityEngine.Random.Range(-terrainZ, terrainZ);

        // Créer la position en prenant en compte la hauteur du terrain à cet endroit.
        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

        // Obtenez la hauteur du terrain à cette position.
        float terrainHeight = terrain.SampleHeight(randomPosition);

        // Mettez à jour la position en utilisant la hauteur du terrain.
        randomPosition.y = terrainHeight;

        return randomPosition;
    }
}
