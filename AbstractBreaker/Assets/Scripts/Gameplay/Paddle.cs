using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    
    public bool IsFreezed { get; set; }

    void Update()
    {
        if(IsFreezed)
        {
            return;
        }

        float dir = Input.GetAxisRaw("Horizontal") * speed;
        float posX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        posX = Mathf.Clamp(posX, -7.5f, 7.5f);
        transform.position = new Vector2(posX, transform.position.y);
    }

    public void Freeze()
    {
        IsFreezed = true;
    }
}
