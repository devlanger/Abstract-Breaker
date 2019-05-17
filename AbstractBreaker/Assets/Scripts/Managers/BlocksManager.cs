using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    public static BlocksManager Instance { get; private set; }
    public Dictionary<int, BlockData> blocks = new Dictionary<int, BlockData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Block SpawnBlock(int id, float x, float y)
    {
        if (blocks.ContainsKey(id))
        {
            BlockData data = blocks[id];
            Block block = Block.Instantiate(Resources.Load<Block>(data.resource), new Vector3(x, y, 0), Quaternion.identity);
            block.Health = data.health;
            block.Destroyable = data.destroyable;

            return block;
        }

        return null;
    }
}
