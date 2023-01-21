using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileType
{
   Empty, Obstacle, player
}

public class Tile : MonoBehaviour
{
    [SerializeField] MeshRenderer meshrenderer;

    //노드의 인덱스
    public Vector2Int Index { get; set; }

    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H; 
    //오픈리스트 여부
    public bool IsOpen { get; set; } = false;
    public bool IsClose { get; set; } = false;  

    public bool IsObstacle { get; set; }
    public void Set(int G, int H)
    {
        if(IsOpen && this.G <G)
        {
            return; 
        }

        this.G = G;
        this.H = H;
        IsOpen = true;

        ReFresh(); 
    }

    public void ReFresh()
    {
        if(IsObstacle)
        {
            meshrenderer.material.color = Color.black;
        }
        else if(IsOpen)
        {
            meshrenderer.material.color = Color.blue;
        }
        else if(IsClose)
        {
            meshrenderer.material.color = Color.red;
        }
        else
        {
            meshrenderer.material.color = Color.white; 
        }
    }
    


}
