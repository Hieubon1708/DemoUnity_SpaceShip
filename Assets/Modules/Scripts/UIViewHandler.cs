using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewHandler : MonoBehaviour
{
    public Text textSound;

    public Slider volume;

    public Button buttonSound;

    private bool isAudioEnabled;

    void Start()
    {
        // ẩn thanh volume lúc đầu 
        SetActiveVolume(false);

        // update lại âm lượng 
        UICommonHandler.OnVolumeUpdate(volume);

        // tăng giảm âm thanh 
        volume.onValueChanged.AddListener(UICommonHandler.OnVolumeChanged);

        Time.timeScale = 1f;
    }

    public void Startgame()
    {
        // letsgo
        // hiện lại Sound và ẩn đi thanh volume
        SoundEnabled(true);
        CommonHandler.NextOrResetLevel(false);
    }

    public void QuitGame()
    {
        // thoát game
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void SetActiveVolume(bool isActive)
    {
        volume.gameObject.SetActive(isActive);
    }

    public void OnClickSound()
    {
        // khi click vào sound thì ẩn sound hiện volume
        UICommonHandler.PlayAudio();
        SoundEnabled(false);
    }

    void SoundEnabled(bool isEnabled)
    {
        buttonSound.enabled = isEnabled;
        textSound.enabled = isEnabled;
        SetActiveVolume(!isEnabled);
    }
}
