using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    [SerializeField]
    Button next;
    [SerializeField]
    InputField fileNameInput;

    public static List<string[]> data = new List<string[]>();
    public static List<string> ratings = new List<string>();
    public static Hashtable collisions = new Hashtable();
    public static int speed = 0;
    private static int sceneIndex = 0;
    private static int speedIndex = 0;
    private static string add = "";
    public static string fileName = "";
    private static HashSet<string> complete = new HashSet<string>();
    private static readonly string[] scenes = {"O4", "O8", "O12"};
    private static readonly int[] speeds = {90, 120, 150, 180};
    private static HashSet<string> completeCheck = new HashSet<string>();
    private readonly string strTag = "End";

    private bool buttonChange = false;
    private bool change = false;
    private bool changed = false;
    private float timer = 0;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "End")
            GameObject.Find(next.name).GetComponentInChildren<Text>().text = "Close";
        else if(SceneManager.GetActiveScene().name == "Start" || SceneManager.GetActiveScene().name == "Buffer")
            GameObject.Find(next.name).GetComponentInChildren<Text>().text = "Next";
        if(SceneManager.GetActiveScene().name == "Start" || SceneManager.GetActiveScene().name == "Buffer" || SceneManager.GetActiveScene().name == "End")
            next.onClick.AddListener(TaskOnClick);
        timer = 0;
        for (int i = 0; i < speeds.Length; i++) {
            for (int j = 0; j < scenes.Length; j++) {
                completeCheck.Add(speeds[i] + scenes[j]);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 240 && (SceneManager.GetActiveScene().name != "Start") && (SceneManager.GetActiveScene().name != "Buffer") && (SceneManager.GetActiveScene().name != "End"))
        {
            AddToData(speeds[speedIndex], scenes[sceneIndex], timer, collisions);
            collisions = new Hashtable();
            complete.Add(add);
            while (!changed)
            {
                sceneIndex = (int)UnityEngine.Random.Range(0, 3);
                speedIndex = (int)UnityEngine.Random.Range(0, 4);
                add = speeds[speedIndex] + scenes[sceneIndex];
                if (!complete.Contains(add))
                {
                    speed = speeds[speedIndex];
                    SceneManager.LoadScene("Buffer");
                    changed = true;
                }
                else if (completeCheck.IsSubsetOf(complete))
                {
                    SceneManager.LoadScene("Buffer");
                    changed = true;
                }
            }
        }
        else if (buttonChange && SceneManager.GetActiveScene().name == "Buffer")
        {
            if (completeCheck.IsSubsetOf(complete))
            {
                WriteCsv(data, ratings);
                complete.Clear();
                data = new List<string[]>();
                ratings = new List<string>();
                SceneManager.LoadScene("End");
                change = false;
            }
            else
            {
                SceneManager.LoadScene(scenes[sceneIndex]);
                change = false;
            }
        }
        else if (buttonChange && SceneManager.GetActiveScene().name == "Start" && (fileNameInput.text != ""))
        {
            //So it goes to the next scene only if the the text field is not empty or and the button is pressed at the Start scene
            fileName = fileNameInput.text;
            data.Add(new string[17] { "Trial", "Speed","Number of Obstacles", "Time", "Difficulty", "Obstacle1","Obstacle2", "Obstacle3","Obstacle4", "Obstacle5",
            "Obstacle6","Obstacle7","Obstacle8","Obstacle9","Obstacle10","Obstacle11","Obstacle12"});
            while (!changed)
            {
                sceneIndex = (int)UnityEngine.Random.Range(0, 3);
                speedIndex = (int)UnityEngine.Random.Range(0, 4);
                add = speeds[speedIndex] + scenes[sceneIndex];
                if (!complete.Contains(add))
                {
                    speed = speeds[speedIndex];
                    SceneManager.LoadScene(scenes[sceneIndex]);
                    change = false;
                    changed = true;
                }
            }
        }
        else if (buttonChange && SceneManager.GetActiveScene().name == "End") {
            change = false;
            changed = true;
            Application.Quit();
        }
        else if (change /*&& (SceneManager.GetActiveScene().name != "Start") && (SceneManager.GetActiveScene().name != "Buffer") && (SceneManager.GetActiveScene().name != "End")*/)
        {
            AddToData(speeds[speedIndex], scenes[sceneIndex], timer, collisions);
            collisions = new Hashtable();
            complete.Add(add);
            while (!changed)
            {
                sceneIndex = (int)UnityEngine.Random.Range(0, 3);
                speedIndex = (int)UnityEngine.Random.Range(0, 4);
                add = speeds[speedIndex] + scenes[sceneIndex];
                if (!complete.Contains(add))
                {
                    speed = speeds[speedIndex];
                    SceneManager.LoadScene("Buffer");
                    change = false;
                    changed = true;
                }
                else if (completeCheck.IsSubsetOf(complete))
                {
                    SceneManager.LoadScene("Buffer");
                    changed = true;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == strTag) {
            change = true;
        }
    }

    void TaskOnClick()
    {
        buttonChange = true;
    }

    private void WriteCsv(List<string[]> d, List<string> r)
    {
        string path = fileName + "." + DateTime.Now.ToString().Replace("/","_").Replace(":","-").Replace(" ",".") + ".csv";
        StringBuilder output = new StringBuilder();
        String sep = ",";
        for (int i = 1; i < data.Count; i++)
        {
            data[i][4] = r[i - 1];
        }
        string[][] dataArray = data.ToArray();
        int length = dataArray.GetLength(0);
        for (int i = 0; i < length; i++)
        {
            output.AppendLine(string.Join(sep, dataArray[i]));
        }
        // Create and write the csv file
        File.WriteAllText(path, output.ToString());
    }

    private void AddToData(int speed, string o,float time, Hashtable c) {
        string[] add = new string[17];
        add[0] = speed + o;
        add[1] = speed + "";
        add[2] = o + "";
        add[3] = "" + time;
        int index = 5;
        foreach (string k in c.Keys) {
            add[index] = "" + c[k];
            index++;
        }
        for (int i = 5; i < add.Length; i++) {
            if (add[i] == null)
                add[i] = "0";
        }
        data.Add(add);
    }

    /*private void WriteInfo(int sp, string sc, float t) {
        try
        {
            FileStream outputStream = File.Open("output.txt", FileMode.Append, FileAccess.Write);
            StreamWriter outputStreamWriter = new StreamWriter(outputStream);
            outputStreamWriter.WriteLine("-----------------------");
            outputStreamWriter.WriteLine("Speed: " + sp);
            outputStreamWriter.WriteLine("Path: " + sc);
            outputStreamWriter.WriteLine("Time in seconds: " + t);
            outputStreamWriter.Flush();
            outputStreamWriter.Close();
        }
        catch (IOException ioe)
        {
            System.Console.WriteLine(ioe.Message);
        }
    }*/

    /*private void WriteCol(Hashtable c) {
    try
    {
        FileStream outputStream = File.Open("output.txt", FileMode.Append, FileAccess.Write);
        StreamWriter outputStreamWriter = new StreamWriter(outputStream);
        foreach (string k in c.Keys) {
            outputStreamWriter.WriteLine("Collided with " + k + ": " + c[k] + " times");
        }
        outputStreamWriter.Flush();
        outputStreamWriter.Close();
    }
    catch (IOException ioe) {
        System.Console.WriteLine(ioe.Message);
    }
}*/

}