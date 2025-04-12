using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(RayCastManager))]
public class Player : Actor {
    InputManager inputManager;
    RayCastManager rayCastManager;
    void Start() {
        inputManager = GetComponent<InputManager>();
        rayCastManager = GetComponent<RayCastManager>();
    }

    void Update() {
        //...
    }

    public void UpdatePosition() {
        Vector2 offset = inputManager.GetDirectionalInput();

        float factor = 0.2f;
        //speedX = offset.x * pixelSize * factor; // input raw 1 move one pixel
        //speedY = offset.y * pixelSize * factor; // input raw 1 move one pixel
        speedX = offset.x * pixelSize; // input raw 1 move one pixel
        speedY = offset.y * pixelSize; // input raw 1 move one pixel
        Vector2 speed = new(speedX, speedY);
        Vector2 maxAvailableMovement = TruncateMovement(speed);

        Vector2 pos = transform.position;
        pos.x += maxAvailableMovement.x;
        pos.y += maxAvailableMovement.y;
        transform.position = pos;
    }

    public void ApplyGravity() {

    }

    public Vector2 TruncateMovement(Vector2 offset) {
        Vector2 truncate1 = rayCastManager.PerformChecksVertical(transform.position, offset);
        Vector2 truncate2 = rayCastManager.PerformChecksHorizontal(transform.position, truncate1);
        return truncate2;
    }
}
