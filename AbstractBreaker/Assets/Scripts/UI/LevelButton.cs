using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int LevelId { get; set; }

    [SerializeField]
    private Text levelNameText;

    [SerializeField]
    private GameObject blockedIndicator;

    private Button bt;

    private void Awake()
    {
        bt = GetComponent<Button>();
    }

    private void Start()
    {
        bt.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        LevelsManager.Instance.LoadLevel(LevelId);
    }

    public void Fill(LevelData level)
    {
        this.LevelId = level.id;
        levelNameText.text = level.name;

        if (!LevelsManager.Instance.UnlockedLevels && level.id > LevelsManager.Instance.UnlockedLevel + 1)
        {
            bt.enabled = false;
            blockedIndicator.SetActive(true);
        }
    }
}
