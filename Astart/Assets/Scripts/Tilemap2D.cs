using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap2D : MonoBehaviour
{

    [SerializeField]
    private GameObject playerPrefab; 
    [SerializeField]
    private GameObject tilePrefab;

    public int Width { private set; get; } = 20; 
    public int Height { private set; get; } = 20;

    void Awake()
    {
        GenerateTilemap();
        SpwanPlayer(TileType.player); 
    }
    public void GenerateTilemap()
    {
        for(int y = 0; y < Height; ++y)
        {
            for (int x = 0; x < Width; ++x)
            {
                //�����Ǵ� Ÿ�ϸ� �߾� (0,0,0) ��ġ
                Vector3 position = new Vector3((-Width * 1f + 1f) + x, (Height * 1f -1f) - y, 0);

                SpwanTile(TileType.Empty, position);
            }
        }
    }
  
    private void SpwanTile(TileType tiletype, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile";
        clone.transform.SetParent(transform); // tilemap2D ������Ʈ�� tile ������Ʈ �θ�� ���� 
    } 
    private void SpwanPlayer(TileType tileType)
    {
        Vector3 position = new Vector3((-Width / 2f + 0.5f), (Height / 2f - 0.5f), 0); 
        GameObject clone = Instantiate(playerPrefab, position, Quaternion.identity); 
    }
 
}
