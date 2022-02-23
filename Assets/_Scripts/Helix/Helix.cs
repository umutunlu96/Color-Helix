using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour
{
    private bool movable = true;
    private float angle;
    private float lastDeltaAngle, lastTouchX;
    private float speed = 1.7f;


    void Update()
    {
        if (movable && Touch.IsPressing())
        {
            float mouseX = this.GetMouseX();
            lastDeltaAngle = lastTouchX - mouseX;
            angle += lastDeltaAngle * 360 * speed;
            lastTouchX = mouseX;
        }
        else if (lastDeltaAngle != 0)//Ekrana dokunmadigimizda yavaslatmak icin
        {
            lastDeltaAngle -= lastDeltaAngle * 5 * Time.deltaTime;
            angle += lastDeltaAngle * 360 * speed;
        }

        transform.eulerAngles = new Vector3(0, 0, angle);

    }

    private float GetMouseX()
    {
        return Input.mousePosition.x / (float)Screen.width;
    }
}
