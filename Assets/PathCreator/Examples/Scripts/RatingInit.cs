using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RatingInit : MonoBehaviour
{
    [SerializeField]
    Vector3 pos;

    private readonly string strTag = "Player";
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
            WriteRating(gameObject.name);
            SceneChange.ratings.Add(gameObject.name);
            collision.collider.gameObject.transform.SetPositionAndRotation(pos, Quaternion.AngleAxis(0, Vector3.up));
        }

    }
    private void WriteRating(string Rating)
    {
        try
        {
            FileStream outputStream = File.Open("output.txt", FileMode.Append, FileAccess.Write);
            StreamWriter outputStreamWriter = new StreamWriter(outputStream);
            outputStreamWriter.WriteLine("Difficulty: " + Rating);
            outputStreamWriter.Flush();
            outputStreamWriter.Close();
        }
        catch (IOException ioe)
        {
            System.Console.WriteLine(ioe.Message);
        }
    }
}
