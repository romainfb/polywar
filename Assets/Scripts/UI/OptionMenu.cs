using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Button audioButton; // Bouton pour activer/désactiver le son
    [SerializeField] private TextMeshProUGUI audioButtonText; // Texte affichant l'état du son
    [SerializeField] private AudioMixerGroup audioMixerGroup; // Groupe de mixage pour le son
    [SerializeField] private Button MusicButton; // Bouton pour activer/désactiver la musique
    [SerializeField] private TextMeshProUGUI MusicButtonText; // Texte affichant l'état de la musique

    /**
     * Modifie l'état du son en fonction de son état actuel.
     * @return void
     */
    public void modifyAudioButton()
    {
        // Inverse le texte du bouton entre "Audio: On" et "Audio: Off"
        audioButtonText.text = audioButtonText.text == "Audio: On" ? "Audio: Off" : "Audio: On";
        // Ajuste le volume en fonction de l'état du bouton
        audioMixerGroup.audioMixer.SetFloat("Volume", audioButtonText.text == "Audio: On" ? 0 : -80);
    }
   
    /**
     * Modifie l'état de la musique en fonction de son état actuel.
     * @return void
     */
    public void modifyMusicButton()
    {
        // Inverse le texte du bouton entre "Music: On" et "Music: Off"
        MusicButtonText.text = MusicButtonText.text == "Music: On" ? "Music: Off" : "Music: On";
    }
}