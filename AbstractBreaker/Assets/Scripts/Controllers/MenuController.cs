using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoToLevelSelection()
    {
        SceneManager.LoadScene(1);
    }

    public void StartNewGame()
    {
        LevelsManager.Instance.UnlockedLevel = 0;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
}
