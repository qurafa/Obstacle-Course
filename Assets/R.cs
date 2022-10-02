using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R : MonoBehaviour
{
    [SerializeField]
    float RotateSpeed = 90;
    // Update is called once per frame
    void Update()
    {
        float angle = RotateSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }
}
