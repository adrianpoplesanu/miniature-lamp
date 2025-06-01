using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Editor]
[System.Serializable]
[RequireComponent(typeof(FileUtils))]
public class Board : MonoBehaviour {
    [SerializeField] public Vector2Int mapSize = new(50, 50);
    [SerializeField] public int[,] values = new int[50, 50];
    FileUtils fileUtils;

    void Awake() {
        for (int i = 1; i < 30; i++) {
            values[ i,  0] = 2;
            values[ 0,  i] = 2;
            values[30,  i] = 2;
            values[ i, 30] = 2;
        }

        //values[ 1,  4] = 2;
        values[ 2,  4] = 2;
        values[ 3,  4] = 2;
        values[ 4,  4] = 2;
        values[ 5,  4] = 2;


        values[ 7,  8] = 2;
        values[ 8,  8] = 2;
        values[ 9,  8] = 2;
        values[10,  8] = 2;
        values[11,  8] = 2;

        values[13, 12] = 2;
        values[14, 12] = 2;
        values[15, 12] = 2;
        values[16, 12] = 2;
        values[17, 12] = 2;


        values[3, 5] = 1;
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
