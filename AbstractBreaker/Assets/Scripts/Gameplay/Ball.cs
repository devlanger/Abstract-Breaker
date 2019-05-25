using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float ballSpeed = 5;

    [SerializeField]
    private TrailRenderer trailRenderer;

    public bool IsStopped { get; private set; }

    public event Action<Block> OnBlockHit = delegate { };

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ClampVelocity();
        PreventLinearStuck();
    }

    private void PreventLinearStuck()
    {
        float dotRight = Vector2.Dot(rb.velocity.normalized, Vector2.right);
        float dotUp = Vector2.Dot(rb.velocity.normalized, Vector2.up);
        if (dotUp > 0.99f/* || dotUp < -0.99f || dotRight > 0.99f || dotRight < -0.99f*/)
        {
            rb.velocity = (Quaternion.Euler(0, 0, 1) * rb.velocity).normalized * ballSpeed;
        }
        else if(dotUp < -0.99f)
        {
            rb.velocity = (Quaternion.Euler(0, 0, -1) * rb.velocity).normalized * ballSpeed;
        }
        
        if(dotRight > 0.99f)
        {
            rb.velocity = (Quaternion.Euler(0, 0, 1) * rb.velocity).normalized * ballSpeed;
        }
        else if(dotRight < -0.99f)
        {
            rb.velocity = (Quaternion.Euler(0, 0, -1) * rb.velocity).normalized * ballSpeed;
        }
    }

    private void ClampVelocity()
    {
        if (IsStopped)
        {
            return;
        }

        if (rb.velocity.magnitude > ballSpeed || rb.velocity.magnitude < ballSpeed)
        {
            rb.velocity = rb.velocity.normalized * ballSpeed;
        }
    }

    public void Stop()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero;

        IsStopped = true;
    }

    public void Launch()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector3.up * ballSpeed;

        IsStopped = false;
    }

    public void EnableTrail()
    {
        trailRenderer.enabled = true;
    }

    public void DisableTrail()
    {
        trailRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Block block = collision.gameObject.GetComponent<Block>();
        if(block != null && block.Destroyable)
        {
            block.Hit();
            OnBlockHit(block);
        }
    }
}
