using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance { get; set; }

    public int ActualCombo { get; set; }
    public int MaxCombo { get; set; }

    [SerializeField]
    private GameManager gameManager;

    private float lastHitTime;

    public event Action<int> OnComboRaised = delegate { };
    public event Action OnComboFinished = delegate { };

    private Coroutine comboCoroutine;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        ActualCombo = 1;
        MaxCombo = 0;

        gameManager.OnBlockHit += GameManager_OnBlockHit;
    }

    private void OnDestroy()
    {
        gameManager.OnBlockHit -= GameManager_OnBlockHit;
    }

    private IEnumerator ComboCoroutine()
    {
        yield return new WaitForSeconds(3);
        OnComboFinished();

        ActualCombo = 1;
    }

    private void GameManager_OnBlockHit(Block obj)
    {
        if (Time.time <= lastHitTime + 3)
        {
            if(comboCoroutine != null)
            {
                StopCoroutine(comboCoroutine);
            }

            comboCoroutine = StartCoroutine(ComboCoroutine());
            ActualCombo++;
            if (ActualCombo > MaxCombo)
            {
                MaxCombo = ActualCombo;
            }
            OnComboRaised(ActualCombo);
        }

        lastHitTime = Time.time;
    }
}
