using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileUtils : MonoBehaviour {
    void Start() {
        /*string path = Application.persistentDataPath + "/example.txt";
        Debug.Log(path);

        // Write to the file
        WriteToFile(path, "Hello, Unity File Writing!");

        // Read from the file to confirm
        string content = ReadFromFile(path);
        Debug.Log("File Content: " + content);*/
    }

    public void WriteToFile(string path, string content) {
        File.WriteAllText(path, content);
        Debug.Log("File written to: " + path);
    }

    public string ReadFromFile(string path) {
        if (File.Exists(path)) {
            return File.ReadAllText(path);
        }
        return "File not found!";
    }
}
