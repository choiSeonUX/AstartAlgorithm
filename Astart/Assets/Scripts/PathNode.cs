using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    private int gCost;
    private int hCost;
    private int fCost; 

    public void CalcultateFcost()
    {
        fCost = gCost + hCost; 
    }


}
