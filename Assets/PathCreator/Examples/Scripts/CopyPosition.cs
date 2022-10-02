using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{

    [SerializeField]
    Transform t = null;

    void Update()
    {
        transform.position = t.position;
    }
}
