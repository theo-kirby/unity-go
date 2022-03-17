using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Stone : MonoBehaviour
{
    public int x,y;
    
    public string color;

    public bool empty, dead;
    
    public int liberties;
    public int libertyCount;
    public int[] libertyArray;
    
    public (int, int) top,right,bottom,left;
    public ((int, int),(int, int),(int, int),(int, int)) directions; 

    public GameObject gameObject;

    public void setDirections()
    {
        this.top = (x,y+1);
        this.right = (x+1,y);
        this.bottom = (x,y-1);
        this.left = (x-1,y);

        this.directions = (this.top,this.right,this.bottom,this.left);
    }
}



