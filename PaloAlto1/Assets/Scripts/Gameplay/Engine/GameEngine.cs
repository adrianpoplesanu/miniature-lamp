using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class GameEngine : MonoBehaviour {
    public GameObject levelSettings;
    public GameObject player;
    public GameObject defaultPlatformPrefab;
    public int[,] board;
    public Vector2Int mapSize;

    public 
    void Start() {
        board = levelSettings.GetComponent<Board>().values;
        mapSize = levelSettings.GetComponent<Board>().mapSize;
        InitializeGame();
    }

    void Update() {
        UpdatePlayerPosition();
        ApplyGravity();
    }

    void InitializeGame() {
        Debug.Log("[ GameEngine ] initializing game...");
        Debug.Log(board);
        for (int i = 0; i < mapSize.x; i++) {
            for (int j = 0; j < mapSize.y; j++) {
                if (board[i, j] == 1) {
                    player.transform.position = new Vector2(j, i);
                } else if (board[i, j] == 2) {
                    Debug.Log("instantiating platform");
                    Vector2 spawnPosition = new(j, i - mapSize.x);
                    Quaternion spawnRotation = Quaternion.identity;

                    GameObject newObject = Instantiate(defaultPlatformPrefab, spawnPosition, spawnRotation);
                }
            }
        }
    }

    void UpdatePlayerPosition() {
        // TODO: fix this ugly reference
        player.GetComponent<Player>().UpdatePosition();
    }

    void ApplyGravity() {
        // TODO: fix this ugly reference
        player.GetComponent<Player>().ApplyGravity();
    }
}
