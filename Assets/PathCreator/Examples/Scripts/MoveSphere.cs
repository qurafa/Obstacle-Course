using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSphere : MonoBehaviour
{
    [SerializeField]
    float velocity = 0;
    [SerializeField]
    Transform wrt;
    [SerializeField]
    KeyCode keyForward;
    [SerializeField]
    KeyCode keyBackward;
    [SerializeField]
    KeyCode keyLeft;
    [SerializeField]
    KeyCode keyRight;
    [SerializeField]
    Text time = null;

    float timer = 0;
    private Color[] colors = {Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow};

    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       timer += Time.deltaTime;
        time.text =  "Time: " + timer;
        /*if (timer > 5) {
            int r = Random.Range(0, colors.Length);
            GetComponent<Renderer>().material.color = colors[r];
            timer = 0;
        }*/
        
        if (Input.GetKey(keyForward))
        {
            GetComponent<Rigidbody>().velocity += velocity * wrt.forward;
            GetComponent<Rigidbody>().velocity -= velocity * -1 * wrt.forward;
        }
        if (Input.GetKey(keyBackward)) {
            GetComponent<Rigidbody>().velocity -= velocity * wrt.forward;
            GetComponent<Rigidbody>().velocity += velocity * -1 *  wrt.forward;
        }
        if (Input.GetKey(keyRight)) {
            GetComponent<Rigidbody>().velocity += velocity * wrt.right;
            GetComponent<Rigidbody>().velocity -= velocity * -1 * wrt.right;
        }
        if (Input.GetKey(keyLeft)) {
            GetComponent<Rigidbody>().velocity -= velocity * wrt.right;
            GetComponent<Rigidbody>().velocity += velocity * -1 * wrt.right;
        }
            
    }

}
