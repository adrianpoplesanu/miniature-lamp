using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RayCastManager : MonoBehaviour
{
    public int horizontalPoints;
    public int verticalPoints;
    public float spriteSizeWidth;
    public float spriteSizeHeight;
    public String[] hitLayers;

    public Vector2 PerformChecksVertical(Vector2 center, Vector2 offset) {
        float rayDistance = Math.Abs(offset.y) * 6f; // x6 for debugging purposes
        Ray ray;
        RaycastHit2D hit;
        Vector2 origin;
        Vector2 result = offset * 6f; // x6 for debugging purposes
        float step = verticalPoints != 1 ? spriteSizeWidth / (verticalPoints - 1) : 0f;
        origin = center;
        for (int i = 0; i < verticalPoints; i++) {
            origin.x = verticalPoints != 1 ? center.x - spriteSizeWidth / 2 + i * step : center.x;

            ray = new Ray(origin, Vector2.down);

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);

            hit = Physics2D.Raycast(origin, Vector2.down, rayDistance, LayerMask.GetMask(hitLayers));
            if (hit.collider != null && hit.distance < Math.Abs(result.y)) {
                result.y = -hit.distance;
            }
        }

        return result;
    }

    public Vector2 PerformChecksHorizontal(Vector2 center, Vector2 offset) {
        float rayDistance = Math.Abs(offset.x) * 6f; // x6 for debugging purposes
        Ray ray;
        RaycastHit2D hit;
        Vector2 origin;
        Vector2 result = offset * 6f; // x6 for debugging purposes
        float step = verticalPoints != 1 ? spriteSizeWidth / (verticalPoints - 1) : 0f;
        origin = center;
        for (int i = 0; i < verticalPoints; i++) {
            origin.x = verticalPoints != 1 ? center.x - spriteSizeWidth / 2 + i * step : center.x;

            ray = new Ray(origin, Vector2.right);

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 1f);

            hit = Physics2D.Raycast(origin, Vector2.right, rayDistance, LayerMask.GetMask(hitLayers));
            if (hit.collider != null && hit.distance < Math.Abs(result.y)) {
                result.y = -hit.distance;
            }
        }

        return result;
    }

    /*private RaycastHit2D PerformCast() {
        
    }*/
}
