using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class Player : Actor {
    InputManager inputManager;
    void Start() {
        inputManager = GetComponent<InputManager>();
    }

    void Update() {
        //Debug.Log(pixelSize);
        //Debug.Log(inputManager.GetDirectionalInput());
    }
}
