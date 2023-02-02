using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Mover
{
    public static GameObject instance;

    public void Awake()
    {
        if (instance != null) return;
        else
        {
            instance = gameObject;
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        UpdateMotor(new Vector3(x,y,0));
    }
}
