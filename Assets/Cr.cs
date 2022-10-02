using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cr : MonoBehaviour
{
    [SerializeField]
    string strTag;
    [SerializeField]
    Vector3 Pos;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == strTag)
        {
            collision.collider.transform.SetPositionAndRotation(Pos, Quaternion.AngleAxis(0, Vector3.up));
            collision.collider.attachedRigidbody.velocity = new Vector3 (0,0,0);
        }
    }
}
