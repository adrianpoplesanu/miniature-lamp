using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Editor]
[System.Serializable]
[RequireComponent(typeof(FileUtils))]
public class Board : MonoBehaviour {
    public Vector2Int mapSize = new(10, 10);
    public int[,] values = new int[20, 20];
    FileUtils fileUtils;

    void Start() {
        //values = new int[mapSize.x, mapSize.y];
        fileUtils = GetComponent<FileUtils>();
    }

    void Update() {
        //Debug.Log(values[12, 12]);
    }

    public void GenerateRandomValues() {
        values = new int[mapSize.x, mapSize.y];
        for (int i = 0; i < mapSize.x; i++) {
            for (int j = 0; j < mapSize.y; j++) {
                values[i, j] = UnityEngine.Random.Range(0, 10);
            }
        }
    }

    public void SaveCurrentBoard() {
        String savePath = "Assets/Levels/Level_Idea_" + System.DateTime.Now.ToString().Replace('/', '-').Replace(' ', '_').Replace(':', 'p') + ".txt";
        Debug.Log(savePath);
        fileUtils.WriteToFile(savePath, "hello world!");
    }
}
