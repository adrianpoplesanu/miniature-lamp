using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Editor]
[System.Serializable]
[RequireComponent(typeof(FileUtils))]
public class Board : MonoBehaviour {
    [SerializeField] public Vector2Int mapSize = new(10, 10);
    [SerializeField] public int[,] values = new int[20, 20];
    FileUtils fileUtils;

    void Awake() {
        values[3, 5] = 1;
        values[8, 1] = 2;
        values[8, 2] = 2;
        values[8, 3] = 2;
        values[8, 4] = 2;
        values[8, 5] = 2;
        values[8, 6] = 2;
        values[8, 7] = 2;
        values[8, 8] = 2;
        values[8, 9] = 2;
    }

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
