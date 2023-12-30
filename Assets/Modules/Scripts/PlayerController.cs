using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Text text;

    public GameObject energyGun;
    public GameObject enemy;
    private GameObject newEnemy;

    private EnemyHandler enemyInstance;

    public Vector3 spawnArea;
    private Vector3 randomPosition;
    private Vector3 direction;
    private Vector3 tempPosition;

    private bool checkEndGun;
    private bool isAudioEnabled;
    public static bool isPlay;

    private int startAmountEnemy;

    public AudioSource audioGun;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        startAmountEnemy = CommonHandler.amountEnemy;
        // thiết lập chỉ số ban đầu của Enemy Enemy: 5/5
        text.text = "Enemy: " + startAmountEnemy + "/" + startAmountEnemy;

        // tắt triển khai súng
        energyGun.SetActive(false);

        // tắt nhạc lúc đầu 
        audioGun.gameObject.SetActive(false);

        // tạo các enemy 
        if(isPlay == false)
        {
            CreatEnemy();
            isPlay = true;
        }
    }

    void Update()
    {
        // nếu SpaceShip hết máu thì sẽ không cho triển khai súng 
        if (CommonHandler.healthPointSpaceShip > 0 && Time.timeScale == 1f)
        {
            if (Input.GetKeyDown("f"))
            {
                // giữ vị trí ban đầu của súng 
                tempPosition = transform.localPosition;
                animator.enabled = false;
            }

            if (Input.GetKey("f"))
            {
                // giúp súng có thể giật trong khoảng 0.01f
                // nếu không có biến tempPosition nó sẽ giật ra khỏi vị trí cũ
                randomPosition = new Vector3(Random.Range(transform.localPosition.x - 0.01f, transform.localPosition.x + 0.01f),
                transform.localPosition.y, transform.localPosition.z);
                transform.localPosition = randomPosition;
                
                if (isAudioEnabled == false)
                {
                    // bật nhạc
                    audioGun.gameObject.SetActive(true);
                    isAudioEnabled = true;
                }

                // triển khai súng 
                energyGun.SetActive(true);
                // phát ra âm thanh 
                audioGun.Play();
                
            }
            if (Input.GetKeyUp("f"))
            {
                // trở về vị trí ban đầu của súng
                transform.localPosition = tempPosition;
                // sau khi nhả phím f thì tắt súng
                energyGun.SetActive(false);
                animator.enabled = true;
            }
        }

        // bảng đếm số lượng enemy còn tồn tại nếu bắn rơi 1 cái sẽ trừ đi 1 
        // biến start cũng để giữ số lượng địch ban đầu ví dụ Enemy: 5/3 
        text.text = "Enemy: " + startAmountEnemy + "/" + CommonHandler.amountEnemy;
    }

    void CreatEnemy()
    {
        for (int i = 0; i < CommonHandler.amountEnemy; i++)
        {
            // tạo obj enemy mới 
            newEnemy = Instantiate(enemy);
            // new class enemy 
            enemyInstance = newEnemy.GetComponent<EnemyHandler>();
            // gán index cho enemy đó để hiện thị trên thanh máu ví dụ Enemy 1 
            enemyInstance.SetIndex(i);

            //Random vị trí xuất hiện
            // spawnArea là vị trí xuất hiện trong khoảng mình tự chỉ định chỉnh sửa ở thuộc tính 
            newEnemy.transform.position = CommonHandler.RandomPosition(spawnArea, 20, spawnArea.y);

            // luôn hướng về spaceShip 
            direction = transform.position - newEnemy.transform.position;
            newEnemy.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
