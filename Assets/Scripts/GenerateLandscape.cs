using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLandscape : MonoBehaviour
{
    public int width = 8;
    public int depth = 8;
    public int heightScale = 20;

    public float detailScale = 25f;

    public GameObject brickBlock;
    public GameObject lowBlock;
    public GameObject hightBlock;
    // Use this for initialization
    void Start()
    {
        int seed = (int) (Network.time*1.1);
        //width *= 20;//местный скейл, я тут шериф
        //depth *= 20;
        for (int z = 0; z <= depth; z++)
        {
            for (int x = 0; x <= width; x++) 
			{
				int y = (int)(Mathf.PerlinNoise((x+seed)/detailScale,(z+seed)/detailScale)*heightScale);
				Vector3 blockPos = new Vector3 (x, y, z);
                createBlock(blockPos);
			}
        }
    }

    void createBlock(Vector3 pos)
    {
        int y = (int)pos.y;

        if(y >15)
             Instantiate(hightBlock, pos, Quaternion.identity);
        else if(y < 5)
                Instantiate(lowBlock, pos, Quaternion.identity);
        else
             Instantiate(brickBlock, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
