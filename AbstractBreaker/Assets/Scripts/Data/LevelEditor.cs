using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public int levelId;
    public string sceneName = "GameScene";
    public string levelName = "";
    
    [ContextMenu("Create Level JSON")]
    public void CreateJson()
    {
        JObject level = new JObject();
        level["id"] = levelId;
        level["name"] = levelName;
        level["sceneName"] = sceneName;

        JArray blocks = new JArray();

        foreach (var block in FindObjectsOfType<Block>())
        {
            JObject blockData = new JObject();
            blockData["id"] = block.Id;
            blockData["pos"] = new JObject();
            blockData["pos"]["x"] = block.transform.position.x;
            blockData["pos"]["y"] = block.transform.position.y;

            blocks.Add(blockData);
        }

        level["blocks"] = blocks;
        
        GUIUtility.systemCopyBuffer = level.ToString();
        Debug.Log("Copied level json to clipboard.");
    }
}
