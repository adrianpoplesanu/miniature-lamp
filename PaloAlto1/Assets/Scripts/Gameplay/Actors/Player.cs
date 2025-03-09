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
        //ApplyGravity();
    }

    public void UpdatePosition() {
        Vector2 pos = transform.position;
        pos.x += speedX;
        pos.y += speedY;
        transform.position = pos;
    }

    public void ApplyGravity() {

    }
}
