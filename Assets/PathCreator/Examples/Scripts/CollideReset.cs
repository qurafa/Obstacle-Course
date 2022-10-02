using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollideReset : MonoBehaviour
{


    [SerializeField]
    Vector3 Pos;

    private readonly string strTag = "Player";
    private int col = 0;
    private Color[] colors = { Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };

    private void Start()
    {
        int r = Random.Range(0, colors.Length);
        GetComponent<Renderer>().material.color = colors[r];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == strTag)
        {
            collision.collider.transform.SetPositionAndRotation(Pos, Quaternion.AngleAxis(0, Vector3.up));
            collision.collider.attachedRigidbody.velocity = new Vector3(0, 0, 0);
            col++;
            if (SceneManager.GetActiveScene().name != "Buffer") {
                if (SceneChange.collisions.ContainsKey(gameObject.name))
                {
                    SceneChange.collisions[gameObject.name] = col;
                }
                else
                {
                    SceneChange.collisions.Add(gameObject.name, col);
                }
            }
        }
    }
}
