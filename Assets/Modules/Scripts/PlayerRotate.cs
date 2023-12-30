using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private float mouseXValue;
    private float mouseYValue;

    void Update()
    {
        // xoay trái phải trên dưới bằng chuột trái và time của chương trình đang chạy 
        if (Input.GetMouseButton(0) && Time.timeScale == 1f && CommonHandler.healthPointSpaceShip > 0)
        {
            mouseXValue = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseXValue * 5f);
            mouseYValue = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.forward * mouseYValue * 3f);

        }

        // đặt trục x luôn luôn = 0
        Vector3 zeroX = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);

        transform.rotation = Quaternion.Euler(zeroX);
    }
}
