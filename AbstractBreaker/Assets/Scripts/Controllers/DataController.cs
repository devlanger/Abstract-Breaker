using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance { get; private set; }

    [SerializeField]
    private TextAsset blocksData;

    [SerializeField]
    private TextAsset levelsData;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadBlocksDataFromJSON();
        LoadLevelsDataFromJSON();
    }

    private void LoadLevelsDataFromJSON()
    {
        JObject levels = JObject.Parse(levelsData.text);
        foreach (var item in levels["levels"])
        {
            LevelData level = new LevelData();
            level.id = (int)item["id"];
            level.name = (string)item["name"];
            level.sceneName = (string)item["sceneName"];
            level.backgroundResource = (string)item["backgroundResource"];
            foreach (var spawnData in item["blocks"])
            {
                LevelData.BlockSpawn spawn = new LevelData.BlockSpawn();
                spawn.blockId = (int)spawnData["id"];
                spawn.x = (float)spawnData["pos"]["x"];
                spawn.y = (float)spawnData["pos"]["y"];
                level.spawns.Add(spawn);
            }

            LevelsManager.Instance.levels.Add(level.id, level);
        }
    }

    private void LoadBlocksDataFromJSON()
    {
        JObject blocks = JObject.Parse(blocksData.text);
        foreach (var item in blocks["blocks"])
        {
            BlockData blockData = new BlockData();
            blockData.id = (int)item["id"];
            blockData.resource = (string)item["resource"];
            blockData.health = (int)item["health"];
            blockData.destroyable = (bool)item["destroyable"];

            BlocksManager.Instance.blocks.Add(blockData.id, blockData);
        }
    }
}
