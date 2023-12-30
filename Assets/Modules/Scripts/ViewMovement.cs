using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMovement : MonoBehaviour
{
    public float speed = 15f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
