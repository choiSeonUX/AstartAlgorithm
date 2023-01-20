using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tilemap2D : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab; 
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private GameObject ObstaclePrefab; 

    //맵 크기 
    public int Width { private set; get; } = 20; 
    public int Height { private set; get; } = 20;
    void Awake()
    {
        GenerateTilemap();
        GenerateObstacle();
        SpwanPlayer(TileType.player); 
    }
    public void GenerateTilemap()
    {
        for(int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                //생성되는 타일맵 중앙 위치
                Vector3 position = new Vector3((-Width * 0.5f + 0.5f) + x, (Height * 0.5f -0.5f) - y, 0);
                SpwanTile(TileType.Empty, position);
            }
        }
    }
  
    //20퍼센트 무작위 장애물 
    public void GenerateObstacle()
    {
        for (int i=0; i < (Width*Height*0.2f); ++i)
        { 
            int randX = Random.Range(-Width/2, Width/2);
            int randY = Random.Range(-Height/2, Height/2);

            if (randX < 0 && randY > 0)
            {
                Vector3 position = new Vector3(randX+0.5f, randY-0.5f, 0);
                SpwanObstacle(TileType.Obstacle, position);
            }
            else if(randX > 0 && randY < 0)
            {
                Vector3 position = new Vector3(randX - 0.5f, randY + 0.5f, 0);
                SpwanObstacle(TileType.Obstacle, position);
            }
            else if(randX < 0 && randY <0)
            {
                Vector3 position = new Vector3(randX + 0.5f, randY + 0.5f, 0);
                SpwanObstacle(TileType.Obstacle, position);
            }
            else if(randX > 0 && randY > 0)
            {
                Vector3 position = new Vector3(randX - 0.5f, randY - 0.5f, 0);
                SpwanObstacle(TileType.Obstacle, position);
            }
        }
    }

    private void SpwanTile(TileType tiletype, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);
        clone.transform.SetParent(transform); // Tilemap2D 오브젝트 밑으로 넣어줌 
    } 
    private void SpwanPlayer(TileType tileType)
    {
        Vector3 position = new Vector3(0, 0, 0); 
        GameObject clone = Instantiate(playerPrefab, position, Quaternion.identity); 
    }
 
    private void SpwanObstacle(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(ObstaclePrefab, position, Quaternion.identity);
        clone.transform.SetParent(transform); 
    }
}
