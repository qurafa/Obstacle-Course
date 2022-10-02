using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Rating : MonoBehaviour
{
    [SerializeField]
    KeyCode keyLeft;
    [SerializeField]
    KeyCode keyRight;

    public Button next = null;
    public Slider rate = null;
    public Text value = null;
    public float r = 4.5f;

    void Start()
    {
        InvokeRepeating("Repeat", r, r);
        GameObject.Find(next.name).GetComponentInChildren<Text>().text = "Next";
        next.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        value.text = rate.value + "";
    }

   void Repeat()
    {
        if (Input.GetKey(keyRight))
        {
            rate.value++;
        }
        if (Input.GetKey(keyLeft))
        {
            rate.value--;
        }
    }

    void TaskOnClick()
    {
        SceneChange.ratings.Add(value.text);
    }
}
