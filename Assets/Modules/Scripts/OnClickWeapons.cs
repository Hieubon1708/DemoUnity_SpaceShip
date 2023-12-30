using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickWeapons : MonoBehaviour
{
    private string text;

    // khi click vào image weapon nó sẽ tự động đổi màu súng
    void Start()
    {
        Button button = GetComponent<Button>();
        text = transform.Find("Text").GetComponent<Text>().text.ToLower();
        button.onClick.AddListener(WeaponSelected);
    }

    public void WeaponSelected()
    {
        UICommonHandler.PlayAudio();
        CommonHandler.currentGunName = text;
        CommonHandler.isSelected = true;
    }
}
