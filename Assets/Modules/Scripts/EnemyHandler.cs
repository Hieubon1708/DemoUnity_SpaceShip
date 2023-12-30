using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public int healthPointEnemy;
    private int kill;
    private int indexEnemy;

    public GameObject bullet;

    public Vector3 spawnArea;
    public Vector3 direction;
    public Vector3 randomPosition;

    private bool isDead;
    private bool isLose;
    

    public void SetIndex(int index)
    {
        this.indexEnemy = index;
        this.healthPointEnemy = 50;
    }

    void Start()
    {
        // đặt giá trị ban đầu cho thanh máu Enemy
        CommonHandler.SetStartValueSlider(healthPointEnemy);

        // đặt vị trí súng của enemy bằng vị trí của enemy
        bullet = Instantiate(bullet, transform.position, transform.rotation);

        // tắt thanh máu
        CommonHandler.SetActiveHealthEnemy(false);
    }

    void Update()
    {
        // kiểm tra xem thắng hay thua thì mặc định tắt thanh máu 
        if (CommonHandler.healthPointSpaceShip == 0 && isLose == false ||
            CommonHandler.amountEnemy == 0)
        {
            isLose = true;
            CommonHandler.SetActiveHealthEnemy(false);
        }
    }

    // hàm xử lý va chạm giữa đạn của SpaceShip và Enemy
    void OnParticleCollision(GameObject obj)
    {
        // nếu va chạm đến từ đạn của SpaceShip 
        if(obj.name == "Particle System")
        {
            // chắc chắn phải hiện thanh máu của Enemy rồi
            CommonHandler.SetActiveHealthEnemy(true);
            // máu của SpaceShip lớn hơn 0 thì mới trừ nếu k xét thì máu sẽ bị âm
            if (healthPointEnemy > 0)
            {
                CommonHandler.UpdateHealthEnemy(--healthPointEnemy);

            } // trên và dưới hiện thi máu bị trừ và chỉ số index của Enemy 
            CommonHandler.ShowIndexEnemy(indexEnemy);

            // cứ mỗi khoảng máu bị mất Enemy sẽ dịch chuyển ra chỗ khác dựa vào kil này 
            kill++;

            // check null nếu k muốn bị lặp lại nhiều lần
            if (gameObject != null && bullet != null)
            {
                // tới đây thì tốc biến đi chỗ khác bùmmmmmmmmmmmmmmmmmmm
                // à không chỗ này là bị nấc khi Enemy bị dính đạn nó sẽ bị nấc theo trục x nghĩa là nấc ngang 

                randomPosition = new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f),
                Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f), transform.position.z);
                transform.position = randomPosition;
                // cập nhật vị trí viên đạn luôn đi theo Enemy khi bị nấc
                bullet.transform.position = randomPosition;

                // cứ mỗi 35 máu Enemy sẽ bấm Flash
                if (kill == 35)
                {
                    // dịch chuyển 
                    randomPosition = CommonHandler.RandomPosition(spawnArea, 20, spawnArea.y);
                    transform.position = randomPosition;
                    // cập nhật vị trí ngòi súng 
                    bullet.transform.position = randomPosition;

                    // cập nhật lại hướng và reset số lần bị bắn về 0
                    direction = CommonHandler.UpdateDirectionEnemyWithSpaceShip(transform);
                    transform.rotation = Quaternion.LookRotation(direction);
                    bullet.transform.rotation = Quaternion.LookRotation(direction);
                    kill = 0;
                }
            }

            // nếu Enemy hết máu tức là chết + biến check để không bị lặp nhiều lần 
            if (healthPointEnemy == 0 && isDead == false)
            {
                // trừ Enemy đi 1 đơn vị Enemy: 5/4
                CommonHandler.amountEnemy--;
                // chết rồi k cho bắn nữa 
                Destroy(bullet);

                // thêm vật lý để Enemy có thể rớt xuống dưới
                gameObject.AddComponent<Rigidbody>();

                // sau đó 4 giây sau mới bị destroy
                Invoke("Destroy", 4f);
                isDead = true;
            }
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
