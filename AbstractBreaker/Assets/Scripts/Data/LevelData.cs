using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int id;
    public string name;
    public string sceneName;
    public string backgroundResource;
    public List<BlockSpawn> spawns = new List<BlockSpawn>();

    public class BlockSpawn
    {
        public int blockId;
        public float x;
        public float y;
    }
}
