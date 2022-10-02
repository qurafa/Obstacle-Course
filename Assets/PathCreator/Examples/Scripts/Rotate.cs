using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    Transform toRotate;
    [SerializeField]
    readonly string strTag = "Player";

    private static int speed;
    private int col = 0;
    private int r = 0;
    private float direction = 0;

    private void Start()
    {
        speed = SceneChange.speed;
        r = (int)UnityEngine.Random.Range(0, 2);
        if (r == 0)
            direction = -1;
        else
            direction = 1;
    }

    void Update()
    {
        if (col > 0 && (direction == 1 || direction == -1))
        {
            float angle = direction * speed * Time.deltaTime;
            toRotate.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            col++;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == strTag) {
            col++;
        }
    }
}
