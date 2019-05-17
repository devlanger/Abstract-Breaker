using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDynamicBackground : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    private void Start()
    {
        LoadBackgroundFromResources();
    }

    private void LoadBackgroundFromResources()
    {
        Sprite backgroundSprite = Resources.Load<Sprite>(LevelsManager.Instance.CurrentLevel.backgroundResource);
        if (backgroundSprite == null)
        {
            Debug.Log("Couldnt find background sprite in Resources.");
            return;
        }

        backgroundImage.sprite = backgroundSprite;
    }
}
