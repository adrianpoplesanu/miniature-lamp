using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Animations;

public class InputManager : MonoBehaviour, IInputManager {
    static readonly int MAX_SHARDS = 10;
    private int maxShards = 10;
    private Vector2[] inputShards = new Vector2[MAX_SHARDS];
    private int ip = 0;
    public int size = 0;
    public int counter = 0;
    public Vector2 GetDirectionalInput() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public bool StoreDirectionsInput() {
        inputShards[ip] = GetDirectionalInput();
        ip++;
        size++;
        counter++;
        if (ip > 9 ) {
            ip = 0;
        }
        if (size > 9) {
            size = 10;
        }
        return true;
    }

    public void PrintInputShards() {
        int start = ip - 1;
        int end = ip - size;
        if (end < 0) end += 10;
        int index = start;
        while (index != end) {
            Debug.Log(inputShards[index]);
            index--;
            if (index < 0) {
                index = 9;
            }
        }
    }

    /*public void Start() { // used for testing purposes
        for (int i = 0; i < 12; i++) {
            StoreDirectionsInput();
        }
        StoreDirectionsInput();
        StoreDirectionsInput();
        PrintInputShards();
    }*/
}