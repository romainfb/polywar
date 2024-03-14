using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Update = Unity.VisualScripting.Update;

public class GameData : MonoBehaviour
{
    [SerializeField] private Slider HealthSlider; // Référence au slider pour la santé du joueur
    [SerializeField] private Slider StaminaSlider; // Référence au slider pour la stamina du joueur
    [SerializeField] private AudioMixerGroup audioMixerGroup; // Groupe de mixage pour gérer le son
    [SerializeField] private AudioSource Soundtrack; // Source audio pour la bande sonore
    [SerializeField] private TextMeshProUGUI KillTextInfo; // Référence au texte affichant le nombre de kills
    public float maxStamina = 1f; // Valeur maximale de la stamina
    public float generationSpeed = 1f; // Vitesse de régénération de la stamina
    public bool isSoundTrackOn; // Indicateur de l'état de la bande sonore
    public static bool isAbleToUseStamina = false; // Indicateur de la possibilité d'utiliser la stamina
    
    private int killNumber; // Nombre de kills
    
    private void Start()
    {
        Soundtrack.Play(); // Démarrer la bande sonore
        isSoundTrackOn = true; // Activer le son
    }

    /**
     * Ajoute un kill au nombre de kills et met à jour l'affichage.
     * @return void
     */
    public void AddKill()
    {
        killNumber++;
        UpdateKillInfo();
    }

    /**
     * Met à jour l'affichage du nombre de kills.
     * @return void
     */
    public void UpdateKillInfo()
    {
        KillTextInfo.text = $"Kills: {killNumber}";
    }
    
    /**
     * Ajuste le volume du son.
     * @param volume Le niveau de volume à définir.
     * @return void
     */
    public void SetVolume(float volume)
    {
        audioMixerGroup.audioMixer.SetFloat("Volume", volume);
    }
    
    /**
     * Active ou désactive le son en fonction du paramètre.
     * @param isMuted True pour désactiver le son, false pour l'activer.
     * @return void
     */
    public void SetVolumeOnorOff(bool isMuted)
    {
        if (isMuted)
        {
            audioMixerGroup.audioMixer.SetFloat("Volume", -80);
        }
        else
        {
            audioMixerGroup.audioMixer.SetFloat("Volume", 0);
        }
    }

    /**
     * Active ou désactive la musique de fond.
     * @return void
     */
    public void SetMusicOnOrOff()
    {
        if (isSoundTrackOn)
        {
            Soundtrack.Stop();
            isSoundTrackOn = false;
        }
        else
        {
            Soundtrack.Play();
            isSoundTrackOn = true;
        }
    }

    /**
     * Réinitialise les données du joueur.
     * @return void
     */
    public  void ResetPlayerData()
    {
        StaminaSlider.value = 1f;
        HealthSlider.value = 1f;
    }

    /**
     * Met à jour la santé du joueur.
     * @param value La quantité de santé à retirer.
     * @return void
     */
    public void UpdateHealth(float value)
    {
        StaminaSlider.value -= value;
    }

    /**
     * Récupère de la santé pour le joueur.
     * @param value La quantité de santé à ajouter.
     * @return void
     */
    public void RecoverHealth(float value)
    {
        HealthSlider.value += value;
        HealthSlider.value = Mathf.Min(HealthSlider.value, maxStamina);
    }

    /**
     * Met à jour la stamina du joueur.
     * @param value La quantité de stamina à retirer.
     * @return void
     */
    public void UpdateStamina (float value)
    {
        StaminaSlider.value -= value;
        if (StaminaSlider.value == 0f) isAbleToUseStamina = false;
    }

    /**
     * Récupère de la stamina pour le joueur.
     * @return void
     */
    public void RecoverStamina()
    {
        StaminaSlider.value += generationSpeed * Time.deltaTime;
        if (StaminaSlider.value > 0.2f) isAbleToUseStamina = true;
        StaminaSlider.value = Mathf.Min(StaminaSlider.value, maxStamina);
    }
}
