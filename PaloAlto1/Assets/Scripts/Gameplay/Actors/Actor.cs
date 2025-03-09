using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
    public float width;
    public float height;
    public int pixelWidth;
    public int pixelHeight;
    public int boardX;
    public int boardY;
    public float posX;
    public float posY;
    public float speedX;
    public float speedY;
    public float maxSpeedX;
    public float maxSpeedY;
    public float minSpeedX;
    public float minSpeedY;
    public float inputFactorSpeedX;
    public float inputFactorSpeedY;
    public int gravityLimit = -6;
    public int gravityAcceleration = -2;
    public int pixelsPerUnit = 32;
    public float pixelSize = 1f / 32f;
}
