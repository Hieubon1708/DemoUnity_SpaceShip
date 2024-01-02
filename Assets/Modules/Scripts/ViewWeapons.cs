using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewWeapons : MonoBehaviour
{
    public List<Sprite> listWeapons;
    public List<Material> listColors;

    public Dropdown colorsWeapons;

    public static GameObject gun;

    private Image imageChildOfWeapon;

    private Text textChildOfWeapon;

    public GameObject weapon;
    public GameObject weaponParent;
    private GameObject newWeapon;

    private bool isCreated;
    private static bool isFirst;

    void Start()
    {
        // thiết lập mặc định lấy phần tử ban đầu 
        if (isFirst == false)
        {
            CommonHandler.currentGunName = listWeapons[0].name.ToLower();
            CommonHandler.currentMaterial = listColors[0];

            isFirst = true;
        }

        // khởi đầu gán giá trị cho 2 biết start nếu người chơi không apply thì sẽ reset về giá trị cũ 
        CommonHandler.startGunName = CommonHandler.currentGunName;
        CommonHandler.startMaterial = CommonHandler.currentMaterial;

        CommonHandler.UpdateKindOfGunAndColor(true);

        // khi có sự kiện selected tự động đổi màu súng 
        colorsWeapons.onValueChanged.AddListener(ColorSelected);
    }


    // đổi súng khi kích vào button ảnh 
    void Update()
    {
        if (CommonHandler.isSelected == true)
        {
            UICommonHandler.PlayAudio();
            CommonHandler.UpdateKindOfGunAndColor(true);
        }
    }

    public void CreatWeapons()
    {
        // giữ giá trị ban đầu để khi người ta xem xong loại súng và màu sắc nhưng k ấn apply
        // mà ấn exit thì sẽ về kiểu ban đầu
        CommonHandler.currentGunName = gun.name;
        CommonHandler.currentMaterial = gun.GetComponent<Renderer>().material;

        if (isCreated == false)
        {
            for (int i = 0; i < listWeapons.Count; i++)
            {
                // tạo đối tượng con từ prefab
                newWeapon = Instantiate(weapon, weaponParent.transform);

                // tìm 2 đối tượng con gồm ảnh và tên của vũ khí
                imageChildOfWeapon = newWeapon.transform.Find("Image").GetComponent<Image>();
                textChildOfWeapon = newWeapon.transform.Find("Text").GetComponent<Text>();

                // Gắn ảnh từ listWeapons vào imageChildOfWeapon
                imageChildOfWeapon.sprite = listWeapons[i];
                // Gắn tên của hình ảnh vào textChildOfWeapon
                textChildOfWeapon.text = listWeapons[i].name;

                newWeapon.AddComponent<WeaponColorsHandler>();
            }
            isCreated = true;
        }

        // Thiết lập mục được chọn là màu cũ 
        colorsWeapons.value = FindIndexMaterial(CommonHandler.startMaterial.name.Replace("(Instance)", "").Trim());
    }

    // duyệt màu súng ra dropdown 
    public void ListColors()
    {
        if (isCreated == false)
        {
            foreach (Material material in listColors)
            {
                colorsWeapons.options.Add(new Dropdown.OptionData()
                {
                    text = material.name
                });

            }
        }
    }

    // hàm tìm vị trí màu súng 
    int FindIndexMaterial(string name)
    {
        for (int i = 0; i < listColors.Count; i++)
        {

            if (listColors[i].name.Equals(name))
            {
                return i;
            }
        }
        return -1;
    }

    // thay màu súng khi chọn 
    public void ColorSelected(int colorSelected)
    {
        gun.GetComponent<Renderer>().material = listColors[colorSelected];
        CommonHandler.currentMaterial = listColors[colorSelected];
    }
}
