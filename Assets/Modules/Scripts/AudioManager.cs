using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            // tạo 1 instance duy nhất dùng cho toàn bộ ứng dụng 
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private float volume = 1f;

    // get và set giá trị chung cho volume
    public float Volume
    {
        get { return volume; }
        set { volume = Mathf.Clamp01(value); }
    }
}
