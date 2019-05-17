using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField]
    private FinishPanel finishPanel;

    [SerializeField]
    private Text timeText;

    private void Awake()
    {
        GameManager.Instance.OnDestroyedBlocks += Instance_OnDestroyedBlocks;
        GameManager.Instance.OnLostGame += Instance_OnLostGame;
        GameManager.Instance.OnGameStarted += Instance_OnGameStarted;
    }

    private IEnumerator StartCounter()
    {
        while (GameManager.Instance.GameStarted)
        {
            timeText.text = GameManager.Instance.GetElapsedTimeString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Instance_OnGameStarted()
    {
        StartCoroutine(StartCounter());
    }

    private void Instance_OnLostGame()
    {
        finishPanel.gameObject.SetActive(true);
        finishPanel.Fill(false);
    }

    private void Instance_OnDestroyedBlocks()
    {
        finishPanel.gameObject.SetActive(true);
        finishPanel.Fill(true);
    }
}
