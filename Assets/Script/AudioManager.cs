using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource MusicBG;
    public AudioSource Walk;
    public AudioSource PlayerAttack;
    public AudioSource EnemyAttack;
    public AudioSource PlayerTakeDamage;
    public AudioSource EnemyTakeDamage;
    public AudioSource SuccesSound;
    public AudioSource GameOverSound;
    public Slider MusicVolumeSlider;
    public Slider MasterVolumeSlider;
    public Slider SFXVolumeSlider;
    public AudioMixer AudioMixer;
    // Start is called before the first frame update
    void Start()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("VolumeMaster", 0);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("VolumeMusic", -10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MusicVolume()
    {
        AudioMixer.SetFloat("VolMusic", MusicVolumeSlider.value);
        PlayerPrefs.SetFloat("VolumeMusic", MusicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void MasterVolume()
    {
        AudioMixer.SetFloat("VolMaster", MasterVolumeSlider.value);
        PlayerPrefs.SetFloat("VolumeMaster", MasterVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void SFXVolume()
    {
        AudioMixer.SetFloat("VolSFX", SFXVolumeSlider.value);
        PlayerPrefs.SetFloat("VolumeSFX", SFXVolumeSlider.value);
        PlayerPrefs.Save();
    }

}
