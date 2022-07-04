using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public Transform LevelStartPoint;
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        GenerateInitialBlocks();
    }

    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count); //numero random entre a y b
        LevelBlock currentBlock = (LevelBlock)Instantiate(allTheLevelBlocks[randomIndex]); //instanciamos un bloque de nivel aleatorio
        currentBlock.transform.SetParent(this.transform, false);  //lo hacemos hijo del level Generator para que esten todos junt
        Vector3 spawnPosition = Vector3.zero;
        if(currentBlocks.Count == 0)
        {
            spawnPosition = LevelStartPoint.position;
        }
        else
        {
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }
        currentBlock.transform.position = spawnPosition;
        currentBlocks.Add(currentBlock);
    }

    public void RemoveOldestBlock()
    {
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while(currentBlocks.Count>0)
        {
            RemoveOldestBlock();
        }
    }

    public void GenerateInitialBlocks()
    {
        for (int i= 0; i<2; i++)
        {
            AddLevelBlock();
        }
    }
}
