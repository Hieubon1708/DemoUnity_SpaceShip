using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Button playContinue;
    public Button quit;
    public Button opttion;
    public Button weapons;
    public Button sound;
    public Button exitWeapons;
    public Button applyWeapons;

    public Text textSound;
    public Text textLevel;
    public Text textPlay;

    public Dropdown colorsWeapons;

    public Slider volume;

    public ScrollRect weaponsScrollView;

    private bool isOpenSound;
    private bool isShowWeapons;

    public ShowWeapons showWeapons;

    void Start()
    {

        // thiết lập hiển thị level cho game
        textLevel.text = "Level" + CommonHandler.gameLevel;

        // update lại âm lượng + tăng giảm âm thanh 
        UICommonHandler.OnVolumeUpdate(volume);
        volume.onValueChanged.AddListener(UICommonHandler.OnVolumeChanged);

        // thoát scrollviewWeapon 
        exitWeapons.onClick.AddListener(ExitWeapons);
        applyWeapons.onClick.AddListener(ApplyWeapons);

        SetActiveVolume(false);
        CheckClickWeapons(false);
        CheckClickOptions(false);

        // ẩn chữ level sau 3 giây
        Invoke("hideLevel", 1.5f);

    }

    void Update()
    {
        // nếu thua thì trong options sẽ có Retry ấn Retry để chơi lại
        if (CommonHandler.healthPointSpaceShip == 0)
        {
            textPlay.text = "Retry";
            CheckClickOptions(true);
        }
    }

    // thoát scroll view mặc định về giá trị ban đầu gồm loại súng và màu súng
    public void ExitWeapons()
    {
        isShowWeapons = false;
        CheckClickWeapons(false);
        Time.timeScale = 1f;
        CommonHandler.UpdateKindOfGunAndColor(false);
        CommonHandler.currentGunName = CommonHandler.startGunName;
        CommonHandler.currentMaterial = CommonHandler.startMaterial;
        UICommonHandler.PlayAudio();
    }

    // đổi súng và màu sắc sau khi apply
    public void ApplyWeapons()
    {
        isShowWeapons = false;
        CheckClickWeapons(false);
        Time.timeScale = 1f;
        CommonHandler.startGunName = CommonHandler.currentGunName;
        CommonHandler.startMaterial = CommonHandler.currentMaterial;
        UICommonHandler.PlayAudio();
    }

    // khi click vào options nó sẽ gọi hàm này và hiển thị lớp options
    // để mình có thể chỉnh sửa âm thanh hoặc thoát game v.v đồng thời tạm ngưng trò chơi
    public void ShowOptions()
    {
        UICommonHandler.PlayAudio();
        hideLevel();
        CheckClickOptions(true);
        PauseGame();
    }

    // click vào weapons sẽ ẩn mấy cái linh tinh đi
    public void OnClickWeapons()
    {
        if(isShowWeapons == false)
        {
            UICommonHandler.PlayAudio();
            hideLevel();
            PauseGame();
            ShowWeapons();
            CheckClickWeapons(true);
            isShowWeapons = true;
        }
    }

    // hiển thị scroll view +  ẩn các thứ bên cạnh đi 
    public void ShowWeapons()
    {
        weaponsScrollView.gameObject.SetActive(true);
        showWeapons.ListColors();
        showWeapons.CreatWeapons();
    }

    // hàm này để ẩn hiện options
    public void CheckClickOptions(bool check)
    {
        sound.gameObject.SetActive(check);
        playContinue.gameObject.SetActive(check);
        quit.gameObject.SetActive(check);
        opttion.gameObject.SetActive(!check);
        weapons.gameObject.SetActive(!check);
    }

    // ân các thứ bên cạnh + hiện scroll view  
    public void CheckClickWeapons(bool check)
    {
        opttion.gameObject.SetActive(!check);
        weapons.gameObject.SetActive(!check);
        exitWeapons.gameObject.SetActive(check);
        applyWeapons.gameObject.SetActive(check);
        colorsWeapons.gameObject.SetActive(check);
        weaponsScrollView.gameObject.SetActive(check);
    }

    // pause game
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // resum game nếu trong options có Continue,  bởi vì khi thua nó sẽ hiện là Retry
    // thua thì phải chơi lại chứ không phải tiếp tục 
    public void ResumeGame()
    {
        if (textPlay.text == "Continue")
        {
            // ấn chơi tiếp đồng thời lại ẩn đi lớp options
            // hiện lại Sound và ẩn đi thanh volume
            SoundEnabled(true);
            CheckClickOptions(false);
            Time.timeScale = 1f;
        }
    }

    public void Startgame()
    {
        // khi ấn Retry nó sẽ chạy vào hàm này
        if (textPlay.text != "Continue")
        {
            // false là chơi lại còn true thì đi tiếp 
            Time.timeScale = 1f;
            CommonHandler.NextOrResetLevel(false);
        }
    }

    public void QuitGame()
    {
        // thoát game
        SceneManager.LoadScene("ViewLayer");
    }

    public void OnClickSound()
    {
        // khi click vào sound thì ẩn sound hiện volume
        SoundEnabled(false);
    }

    void SoundEnabled(bool isEnabled)
    {
        UICommonHandler.PlayAudio();
        textSound.enabled = isEnabled;
        sound.enabled = isEnabled;
        SetActiveVolume(!isEnabled);
    }

    void SetActiveVolume(bool isActive)
    {
        volume.gameObject.SetActive(isActive);
    }

    void hideLevel()
    {
        textLevel.enabled = false;
    }
}
