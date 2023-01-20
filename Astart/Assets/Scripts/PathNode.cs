using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� Ŭ���� 
public class PathNode 
{
    [SerializeField]
    private GameObject emty;

    private bool walkable;
    private int width; 
    private int height;

    public PathNode(GameObject _emty, bool _walkable, int _width, int _height)
    {
        emty = _emty;
        walkable = _walkable;
        width = _width;
        height = _height; 
    }


}
