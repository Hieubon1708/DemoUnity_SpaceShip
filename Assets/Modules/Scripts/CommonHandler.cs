using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class CommonHandler : MonoBehaviour
{
    public static int gameLevel = 1;

    public static int startAmountHealth = 200;
    public static int startAmountEnemy = 2;
    // 2 cặp trên dưới phải giống nhau nhé nếu thay đổi 
    public static int healthPointSpaceShip = 200;
    public static int amountEnemy = 2;

    // thanh máu của enemy 
    public static Slider enemyHealthBackGround;
    public static Slider enemyHealth;
    public static Text enemyIndex;

    public static Material currentMaterial;
    public static string currentGunName;
    // trên dưới là loại súng và màu súng 
    public static Material startMaterial;
    public static string startGunName;

    public static List<GameObject> listGuns;

    public static GameObject spaceShip;

    public static bool isSelected;

    void Awake()
    {
        listGuns = GameObject.FindGameObjectsWithTag("Gun").ToList();
        enemyHealthBackGround = GameObject.Find("EnemyHealthBackground").GetComponent<Slider>();
        enemyHealth = GameObject.Find("EnemyHealth").GetComponent<Slider>();
        enemyIndex = GameObject.Find("NumberHealthEnemy").GetComponent<Text>();
        spaceShip = GameObject.Find("Luminaris Starship");
    }

    public static void SetStartValueSlider(int health)
    {
        enemyHealth.maxValue = health;
        enemyHealth.value = health;
        enemyHealthBackGround.maxValue = health;
        enemyHealthBackGround.value = health;
    }

    // hàm này để tạo ra màn chơi mới hoặc quay trở về màn chơi ban đầu 
    public static void NextOrResetLevel(bool isNext)
    {
        // kiểm tra xem hàm gọi tới có phải để đi tiếp hay không nếu có nhân thêm máu và địch 
        if (isNext == true)
        {
            gameLevel++;
            startAmountHealth *= 2;
            startAmountEnemy *= 2;
        }

        // 2 biến start có nhiệm vụ giữ giá trị đầu tiên để khi bắn nhau hết máu hay địch chết hết thì nó không bị về 0 
        healthPointSpaceShip = startAmountHealth;
        amountEnemy = startAmountEnemy;
        PlayerController.isPlay = false;
        // chuyển màn
        SceneManager.LoadScene("SpaceShip");
    }

    // random vị trí
    public static Vector3 RandomPosition(Vector3 spawnArea, float yMin, float yMax)
    {
        return new Vector3(Random.Range(-spawnArea.x, spawnArea.x),
               Random.Range(yMin, yMax), Random.Range(-spawnArea.z, spawnArea.z));
    }

    // ẩn hiện thanh máu của enemy
    public static void SetActiveHealthEnemy(bool isActive)
    {
        enemyHealthBackGround.gameObject.SetActive(isActive);
        enemyHealth.gameObject.SetActive(isActive);
        enemyIndex.gameObject.SetActive(isActive);
    }

    // trừ đi máu của Enemy khi bị bắn
    public static void UpdateHealthEnemy(int health)
    {
        enemyHealth.value = health;
    }

    // hiện tên + chỉ số trên thanh máu của Enemy 
    public static void ShowIndexEnemy(int index)
    {
        enemyIndex.text = "Enemy " + (index + 1);
    }

    // lấy khoảng cách giữa spaceship và enemy
    public static Vector3 UpdateDirectionEnemyWithSpaceShip(Transform enemy)
    {
        return spaceShip.transform.position - enemy.position;
    }

    // set up các loại súng và màu 
    public static void UpdateKindOfGunAndColor(bool isSetUp)
    {
        foreach (GameObject obj in listGuns)
        {
            if (obj.name.Equals(currentGunName) && isSetUp == true)
            {
                obj.SetActive(true);
                ShowWeapons.gun = obj;
                ShowWeapons.gun.GetComponent<Renderer>().material = currentMaterial;
            }
            else if (obj.name == startGunName && isSetUp == false)
            {
                obj.SetActive(true);
                obj.GetComponent<Renderer>().material = startMaterial;
            }
            else
            {
                obj.SetActive(false);
            }
        }

        isSelected = false;
    }
}
