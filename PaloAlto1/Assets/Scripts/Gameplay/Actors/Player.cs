using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
        //Debug.Log("checking player gravity constraints...");

        /*Vector2 direction = Vector2.right; // Default direction (right)
        float rayDistance = 5f;
        Color rayColor = Color.red;

        Debug.DrawRay(transform.position, direction.normalized * rayDistance, rayColor);*/

        /*Ray ray = new Ray(transform.position, Vector2.down);

        float rayDistance = 3.0f;
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, LayerMask.GetMask("Blocks"));
        
        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
        }*/
        Vector2 hitDistance = rayCastManager.PerformChecksVertical(transform.position, Vector2.down);
        Debug.Log(hitDistance);
    }
}
