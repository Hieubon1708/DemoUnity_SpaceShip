using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICommonHandler : MonoBehaviour
{
    private static AudioManager audioManager;

    private static AudioSource audioClick;

    private static bool isAudioEnabled;

    void Start()
    {
        GameObject audioClickSource = GameObject.Find("AudioClick");
        audioClickSource.SetActive(false);
        audioClick = audioClickSource.GetComponent<AudioSource>();
        isAudioEnabled = false;
    }

    // update lại âm lượng sau mỗi lần chuyển màn 
    public static void OnVolumeUpdate(Slider volume)
    {   
        if(audioManager != null)
        {
            OnVolumeChanged(audioManager.Volume);
            volume.value = audioManager.Volume;
        }
        else
        {
            audioManager = AudioManager.Instance;
        }
    }

    public static void OnVolumeChanged(float volume)
    {
        if(audioManager == null)
        {
            audioManager = AudioManager.Instance;
        }
        // lặp qua tất cả các AudioSource trong game
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            // đặt giá trị âm lượng của AudioSource theo giá trị của Slider
            audioSource.volume = volume;
        }
        audioManager.Volume = volume;
        if (Input.GetMouseButtonDown(0))
        {
            PlayAudio();
        }
    }

    // bật nhạc khi click 
    public static void PlayAudio()
    {
        if (audioClick != null)
        {
            if (isAudioEnabled == false)
            {
                // bật nhạc 
                audioClick.gameObject.SetActive(true);
                isAudioEnabled = true;
            }
            audioClick.Play();
        }
    }
}
