using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance { get; private set; }
    public Dictionary<int, LevelData> levels = new Dictionary<int, LevelData>();

    public event Action<LevelData> OnLevelLoaded = delegate { };

    public int currentLevelId = 1;
    public LevelData CurrentLevel
    {
        get
        {
            return levels[currentLevelId];
        }
    }

    public int UnlockedLevel
    {
        get
        {
            return unlockedLevel;
        }
        set
        {
            unlockedLevel = value;
        }
    }

    private int unlockedLevel = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("unlocked_level"))
        {
            unlockedLevel = PlayerPrefs.GetInt("unlocked_level");
        }

        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void LoadLevel(int levelId)
    {
        currentLevelId = levelId;
        StartCoroutine(LoadYourAsyncScene(levels[levelId]));
    }

    public void FinishLevel()
    {
        if(UnlockedLevel < currentLevelId)
        {
            UnlockedLevel = currentLevelId;
        }

        PlayerPrefs.SetInt("unlocked_level", UnlockedLevel);
    }

    IEnumerator LoadYourAsyncScene(LevelData data)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(data.sceneName);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        OnLevelLoaded(data);
    }
}
