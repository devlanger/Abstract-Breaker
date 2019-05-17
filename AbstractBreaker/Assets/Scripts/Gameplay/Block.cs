using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Id;
    public int Health { get; set; }
    public bool Destroyable { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            Health--;
            if(Health <= 0)
            {
                GameManager.Instance.RemoveBlock(this);
                Destroy(gameObject);
            }
        }
    }
}
