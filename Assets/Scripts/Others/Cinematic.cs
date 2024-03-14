using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Référence vers le composant VideoPlayer pour la lecture de la vidéo.
    public RawImage rawImage; //  Référence vers le composant RawImage pour l'affichage de la vidéo.

    /**
     * Méthode appelée au démarrage de l'objet pour initialiser la lecture de la vidéo.
     */
    void Start()
    {
        if (videoPlayer != null && rawImage != null)
        {
            videoPlayer.targetTexture = new RenderTexture((int)videoPlayer.width, (int)videoPlayer.height, 0);
            rawImage.texture = videoPlayer.targetTexture;
            videoPlayer.Play();
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("Veuillez assigner un composant VideoPlayer et RawImage dans l'éditeur Unity.");
        }
    }

    /**
     * Méthode appelée à chaque frame pour vérifier si la touche Escape est enfoncée.
     */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopVideoAndHideImage();
        }
    }

    /**
     * Méthode appelée lorsque la vidéo est terminée pour arrêter la lecture et charger la scène de jeu.
     * @param vp Le VideoPlayer associé à l'événement.
     * @return void
     */
    void OnVideoEnd(VideoPlayer vp)
    {
        StopVideoAndHideImage();
        SceneManager.LoadScene("Game");
    }

    /**
     * Arrête la lecture de la vidéo et désactive l'affichage de la RawImage.
     * @return void
     */
    private void StopVideoAndHideImage()
    {
        videoPlayer.Stop();
        rawImage.enabled = false;
    }
}