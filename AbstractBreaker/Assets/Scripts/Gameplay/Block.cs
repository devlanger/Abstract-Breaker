using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Id;
    public int Health { get; set; }
    public bool Destroyable { get; set; }

    [SerializeField]
    private GameObject destroyParticlePrefab;

    public void Hit()
    {
        if (Destroyable)
        {
            Health--;
            transform.localScale = Vector3.one;
            transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);

            if (Health <= 0)
            {
                GameManager.Instance.RemoveBlock(this);
                Destroy(gameObject);
                if (destroyParticlePrefab != null)
                {
                    GameObject inst = Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
                    Destroy(inst, 1);
                }
            }
        }
    }
}
