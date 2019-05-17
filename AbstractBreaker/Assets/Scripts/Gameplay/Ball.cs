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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ClampVelocity();
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
}
