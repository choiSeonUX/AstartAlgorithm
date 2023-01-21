using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class TileManager : MonoBehaviour
{
    [SerializeField] Tile tilePrefab;

    private List<Tile> tiles = new List<Tile>(TILE_X * TILE_Y);

    const float TILE_SIZE = 1.0f; 
    const int TILE_X = 20;
    const int TILE_Y = 20;
    const int CENTER_X = 10;
    const int CENTER_Y = 10;
    const float CAM_HEIGHT = 20.0f; 

    private void Start()
    {
        for(int i = 0; i < TILE_X; ++i)
        {
            for(int j = 0; j <TILE_Y; ++j)
            {
                Tile tile = Instantiate(tilePrefab);
                tile.transform.position = new Vector3(i, 0, j);
                tile.Index = new Vector2Int(i, j); 
                tiles.Add(tile); 

                if(Random.Range(0,10) <2)
                {
                    if (i == CENTER_X && j == CENTER_Y) continue; // 플레이어 자리 
                    tile.IsObstacle = true;
                    tile.ReFresh();
                }
                
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousPos = Input.mousePosition;
            mousPos.z = CAM_HEIGHT;
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousPos);
            Vector2Int index = Vector2Int.zero;
            index.x = Mathf.RoundToInt(pos.x / TILE_SIZE);
            index.y = Mathf.RoundToInt(pos.z / TILE_SIZE);
            Tile tile = tiles.Find(_ => _.Index == index);

            FindPath(new Vector2Int() { x = CENTER_X, y = CENTER_Y }, tile.Index); 

        }
    }

    private List<Tile> FindPath(Vector2Int start, Vector2Int end)
    {
        List<Tile> result = new List<Tile>();

        Tile startTile = tiles.Find(_ => _.Index == start);
        Tile endTile = tiles.Find(_ => _.Index == end);

        List<Tile> openList = new List<Tile>();

        startTile.Set(0, GetH(startTile.Index, endTile.Index));

        do
        {
            startTile.IsClose = true;
            endTile.IsOpen = false;

            openList.Remove(startTile);

            startTile.ReFresh();
            if (startTile.Index == end) break;

            //주변탐색 시작

            //대각선 이동 시 상화좌우에 장애물 있을 경우 
            bool isLT = true;
            bool isRT = true;
            bool isLB = true;
            bool isRB = true;

            // 위
            if (SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x, startTile.Index.y + 1)), end) == false)
            {
                isLT = false;
                isRT = false;

            }
            // 아래
            if (SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x, startTile.Index.y - 1)), end) == false)
            {
                isLT = false;
                isRT = false;
            }
            // 왼쪽
            if (SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x - 1, startTile.Index.y)), end) == false)
            {
                isLT = false;
                isLB = false;
            }
            // 오른쪽 
            if (SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x + 1, startTile.Index.y)), end) == false)
            {
                isRT = false;
                isRB = false;
            }

            if (isLT)
            {
                SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x - 1, startTile.Index.y + 1)), end);
            }
            if (isRT)
            {
                SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x + 1, startTile.Index.y + 1)), end);
            }
            if (isLB)
            {
                SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x - 1, startTile.Index.y + 1)), end);
            }
            if (isRB)
            {
                SetTile(openList, startTile, tiles.Find(_ => _.Index == new Vector2Int(startTile.Index.x + 1, startTile.Index.y - 1)), end);
            }

            //// 원래는 우선순위 큐로 F값이 가장 낮은 것을 뽑아야함 
            //var OpenTiles = tiles.Where(_ => _.IsOpen); 
            //if(OpenTiles.Count() > 0)
            //{
            //   int f = OpenTiles.Min(_ => _.F);
            //}
            //startTile = tiles.Find(_ => _.IsOpen); 

            // 우선순위 큐로 구현하면 사라지는 코드 
            int minF = int.MaxValue;

            foreach (var tile in openList)
            {
                if (tile.F < minF)
                {
                    minF = tile.F;
                    startTile = tile;
                }
            }
        }
        while (openList.Count > 0); 

        // 결과 만들어주기 
        return result; 
    }

    private int GetH(in Vector2Int index,in Vector2Int end)
    {
        return Mathf.Abs(index.x - end.x) + Mathf.Abs(index.y - end.y); 
    }

    private int GetG(in Vector2Int parent, in Vector2Int child)
    {
        int result = Mathf.Abs(parent.x - child.x) + Mathf.Abs(parent.y - child.y);
        if (result > 1)
        {
            return 14;
        }
        else
            return 10; 
    }

    private bool SetTile(in List<Tile> openList, Tile parent, Tile child, Vector2Int end)
    {
        if (child == null) return false;
        if (child.IsObstacle) return false;
        if (child.IsClose) return false;

        if(child.IsOpen == false)
        {
            openList.Add(child);
        }
        child.Set(GetG(parent.Index, child.Index), GetH(child.Index, end));
        return true; 
    }
}
