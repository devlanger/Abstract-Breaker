using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsUiPanel : MonoBehaviour
{
    [SerializeField]
    private LevelButton buttonPrefab;

    [SerializeField]
    private Transform panel;

    private void Start()
    {
        foreach (var item in LevelsManager.Instance.levels.Values)
        {
            LevelButton bt = LevelButton.Instantiate(buttonPrefab, panel);
            bt.transform.localScale = Vector3.one;
            bt.Fill(item);
        }
    }
}
