using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private Ball ball;

    [SerializeField]
    private Paddle paddle;

    [SerializeField]
    private GameObject gameCanvas;

    private int blocksCount = 0;

    private float gameStartTime;

    public event Action OnDestroyedBlocks = delegate { };
    public event Action OnLostGame = delegate { };
    public event Action OnGameStarted = delegate { };

    public bool GameStarted { get; private set; }
    public bool GameFinished { get; private set; }
    
    public float TimeElapsed
    {
        get
        {
            return Time.time - gameStartTime;
        }
    }
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ball.DisableTrail();
        ball.Stop();
        ball.transform.parent = paddle.transform;
        ball.transform.position = paddle.transform.position + Vector3.up;

        GameObject inst = GameObject.Instantiate(gameCanvas);

        SpawnLevel(LevelsManager.Instance.CurrentLevel);

        OnDestroyedBlocks += GameManager_OnDestroyedBlocks;
    }

    private void Update()
    {
        HandleInput();
        CheckBallLevel();
    }

    public string GetElapsedTimeString()
    {
        int minutes = Mathf.RoundToInt(TimeElapsed / 60f);
        int seconds = Mathf.RoundToInt(TimeElapsed % 60);

        return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
    }

    private void GameManager_OnDestroyedBlocks()
    {
        WinGame();
    }

    private void WinGame()
    {
        GameFinished = true;

        ball.DisableTrail();
        ball.Stop();
        paddle.Freeze();

        GameStarted = false;

        LevelsManager.Instance.FinishLevel();
    }
    
    private void LoseGame()
    {
        GameFinished = true;

        ball.DisableTrail();
        ball.Stop();
        paddle.Freeze();

        OnLostGame();
        GameStarted = false;
    }

    private void SpawnLevel(LevelData level)
    {
        foreach (var item in level.spawns)
        {
            Block block = BlocksManager.Instance.SpawnBlock(item.blockId, item.x, item.y);

            if (block.Destroyable)
            {
                blocksCount++;
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!GameStarted && !GameFinished)
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        gameStartTime = Time.time;
        GameStarted = true;

        ball.transform.parent = null;
        ball.Launch();
        ball.EnableTrail();

        OnGameStarted();
    }

    private void CheckBallLevel()
    {
        if (GameStarted)
        {
            if (ball.transform.position.y < paddle.transform.position.y - 0.5f)
            {
                LoseGame();
            }
        }
    }

    public void RemoveBlock(Block block)
    {
        if(!block.Destroyable)
        {
            return;
        }

        blocksCount--;

        if(blocksCount <= 0)
        {
            OnDestroyedBlocks();
            GameStarted = false;
        }
    }
}
