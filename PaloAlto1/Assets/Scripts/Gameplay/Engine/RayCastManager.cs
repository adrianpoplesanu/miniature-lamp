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
    public float skin;
    public String[] hitLayers;

    public Vector2 PerformChecksVertical(Vector2 center, Vector2 offset) {
        float rayDistance = Math.Abs(offset.y) * 1f; // x6 for debugging purposes
        Ray ray;
        RaycastHit2D hit;
        Vector2 origin;
        Vector2 result = offset * 1f; // x6 for debugging purposes
        float step = verticalPoints != 1 ? spriteSizeWidth / (verticalPoints - 1) : 0f;
        origin = center;
        origin.y += offset.y > 0 ? spriteSizeWidth / 2 : - spriteSizeWidth / 2;
        for (int i = 0; i < verticalPoints; i++) {
            origin.x = verticalPoints != 1 ? center.x - spriteSizeWidth / 2 + i * step : center.x;
            origin.x += ApplySkin(horizontalPoints, i);

            ray = new Ray(origin, offset.y > 0 ? Vector2.up : Vector2.down);

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 0f);

            hit = Physics2D.Raycast(origin, offset.y > 0 ? Vector2.up : Vector2.down, rayDistance, LayerMask.GetMask(hitLayers));
            if (hit.collider != null && hit.distance < Math.Abs(result.y)) {
                //result.y = -hit.distance;
                result.y = offset.y > 0 ? hit.distance : -hit.distance;
            }
        }

        return result;
    }

    public Vector2 PerformChecksHorizontal(Vector2 center, Vector2 offset) {
        float rayDistance = Math.Abs(offset.x) * 1f; // x6 for debugging purposes
        Ray ray;
        RaycastHit2D hit;
        Vector2 origin;
        Vector2 result = offset * 1f; // x6 for debugging purposes
        float step = horizontalPoints != 1 ? spriteSizeWidth / (horizontalPoints - 1) : 0f;
        origin = center;
        origin.x += offset.x > 0 ? spriteSizeWidth / 2 : - spriteSizeWidth / 2;
        for (int i = 0; i < horizontalPoints; i++) {
            origin.y = horizontalPoints != 1 ? center.y - spriteSizeWidth / 2 + i * step : center.y;
            origin.y += ApplySkin(horizontalPoints, i);

            ray = new Ray(origin, offset.x > 0 ? Vector2.right : Vector2.left);

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 0f);

            hit = Physics2D.Raycast(origin, offset.x > 0 ? Vector2.right : Vector2.left, rayDistance, LayerMask.GetMask(hitLayers));
            if (hit.collider != null && hit.distance < Math.Abs(result.x)) {
                //result.x = -hit.distance;
                result.x = offset.x > 0 ? hit.distance : -hit.distance;
            }
        }

        return result;
    }

    private float ApplySkin(int totalPoints, int index) {
        if (totalPoints > 1) {
            if (index == 0) {
                return skin;
            }
            if (index == totalPoints - 1) {
                return -skin;
            }
        }
        return 0f;
    }
}
