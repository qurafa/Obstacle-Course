using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    Transform toRotateAbout = null;
    [SerializeField]
    public float horizontalSpeed = 1;
    [SerializeField]
    public float verticalSpeed = 1;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        transform.RotateAround(toRotateAbout.position, Vector3.up, mouseX);
    }
}
