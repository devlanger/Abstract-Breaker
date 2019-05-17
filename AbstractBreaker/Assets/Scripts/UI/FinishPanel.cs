using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishPanel : MonoBehaviour
{
    [SerializeField]
    private Text levelNameText;

    [SerializeField]
    private Text gameStateText;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private string winText;

    [SerializeField]
    private string loseText;

    public void GoToLevelSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void Fill(bool win)
    {
        gameStateText.text = win ? winText : loseText;
        levelNameText.text = LevelsManager.Instance.CurrentLevel.name;
        string time = GameManager.Instance.GetElapsedTimeString();
        timeText.text = string.Format("Time: {0}", time);
    }
}
