using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class GameEngine : MonoBehaviour {
    public GameObject levelSettings;
    public GameObject playerGameObject;
    public Player player;
    public GameObject defaultPlatformPrefab;
    public int[,] board;
    public Vector2Int mapSize;

    public 
    void Start() {
        player = playerGameObject.GetComponent<Player>();
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
                    playerGameObject.transform.position = new Vector2(j, i);
                } else if (board[i, j] == 2) {
                    Debug.Log("instantiating platform");
                    Vector2 spawnPosition = new(j, i - mapSize.x);
                    Quaternion spawnRotation = Quaternion.identity;

                    GameObject newObject = Instantiate(defaultPlatformPrefab, spawnPosition, spawnRotation); 
                    // TODO: store this object for references ^^^
                }
            }
        }
    }

    void UpdatePlayerPosition() {
        player.UpdatePosition();
    }

    void ApplyGravity() {
        player.ApplyGravity();
    }
}
