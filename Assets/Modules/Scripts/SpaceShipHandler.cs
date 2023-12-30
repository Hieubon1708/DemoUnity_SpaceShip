using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipHandler : MonoBehaviour
{
    private bool isLose;
    private bool isWin;

    public Slider healthSpaceShip;
    public Slider healthSpaceShipBackground;

    public Text numberHealthSpaceShip;

    public GameObject gun;

    void Start()
    {
        // cài đặt chỉ số thanh máu của SpaceShip ban đầu 
        SetUpHealthPoint(CommonHandler.healthPointSpaceShip);
    }

    void Update()
    {
        // khi thắng nó sẽ gọi hàm này và cái check để cho chỉ chạy 1 lần vào đoạn code này 
        if (CommonHandler.amountEnemy == 0 && isWin == false)
        {
            isWin = true;
            // ẩn thanh máu 
            SetActiveForHealth(false);

            // sau khi thắng thì sẽ chuyển màn sau 5 giây 
            Invoke("NextLevel", 5f);
        }
    }

    // hàm này xử lý va chạm giữa các viên đạn từ địch và SpaceShip 
    void OnParticleCollision(GameObject obj)
    {
        // nếu trên 0 máu thì cho trừ(trừ chỉ số máu và thanh máu),
        // nếu không xét điều kiện này nó sẽ trừ tới âm nhìn sẽ không đẹp
        if (CommonHandler.healthPointSpaceShip > 0)
        {
            healthSpaceShip.value = --CommonHandler.healthPointSpaceShip;
            numberHealthSpaceShip.text = "" + CommonHandler.healthPointSpaceShip;
        }

        // hàm này để kiểm tra xem Space Ship có hết máu chưa và
        // biến check cho đoạn code triển khai 1 lần
        if (CommonHandler.healthPointSpaceShip == 0 && isLose == false)
        {
            isLose = true;
            SetActiveForHealth(false);
            // thua thì không triển khai súng nữa
            gun.gameObject.SetActive(false);

            // sau khi thua gắn cục sắt vô cho nó rơi 
            gameObject.AddComponent<Rigidbody>();
            // ẩn đi sau 20 giây
            Invoke("Hide", 20f);
        }
    }
    void Hide()
    {
        // ẩn spaceShip khi thua
        gameObject.SetActive(false);

    }

    void NextLevel()
    {
        // đi tiếp màn mới
        CommonHandler.NextOrResetLevel(true);
    } 

    // thiết lập các chỉ số thanh máu cho SpaceShip
    void SetUpHealthPoint(int healthPoint)
    {
        healthSpaceShip.maxValue = healthPoint;
        healthSpaceShipBackground.maxValue = healthPoint;
        healthSpaceShipBackground.value = healthPoint;
        healthSpaceShip.value = healthPoint;
        numberHealthSpaceShip.text = "" + healthPoint;
    }

    // thiết lập thanh máu cho spaceShip trong trường hợp muốn ẩn hay hiện  
    void SetActiveForHealth(bool isActive)
    {
        healthSpaceShip.gameObject.SetActive(isActive);
        numberHealthSpaceShip.gameObject.SetActive(isActive);
        healthSpaceShipBackground.gameObject.SetActive(isActive);
    }
}
