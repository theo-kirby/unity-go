using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Stone : MonoBehaviour
{
    public string color;
    public int x;
    public int y;

    public GameObject go;

    public int ll = 0;
    public int rl = 0;
    public int ul = 0;
    public int dl = 0;

    public Vector2Int left;
    public Vector2Int right;
    public Vector2Int up;
    public Vector2Int down;

    public Stone leftStone;
    public Stone rightStone;
    public Stone upStone;
    public Stone downStone;

    public Stone[] stoneSides;
    public GameObject[] sides;
    public GameObject[,] points;


    public int[] libs = new int[4]{0,0,0,0};
    public int liberties = 0;
    public int libCount;
    public int gLiberties = 0;

    public int checkLiberties(GameObject[,] points)
    {
        this.libCount = 0;
        this.points = points;
        string empty = "empty";
        string black = "black";
        string white = "white";

        Vector2Int coords = new Vector2Int(this.x,this.y);
        GameObject l = points[this.x-1,this.y];
        GameObject r = points[this.x+1,this.y];
        GameObject u = points[this.x,this.y+1];
        GameObject d = points[this.x,this.y-1];
        GameObject[] sides = new GameObject[4]{l,r,u,d};

        foreach (var side in sides)
        {
            if (side.tag == empty)
            {
                this.libs[this.libCount] = 1;
                this.libCount++;
            }
            else if (side.tag != this.color)
            {
                this.libs[this.libCount] = 0;
                this.libCount++;
            }
            else if (side.tag == this.color)
            {
                this.libs[this.libCount] = 0;
                this.libCount++;

                foreach (var st in this.stoneSides)
                    if (st != null)
                        if (st.color == this.color)
                            if (st.x == side.transform.position.x && st.y == side.transform.position.y)
                                this.gLiberties += st.checkLiberties(points);
                                
            }
        }
        this.liberties = this.libs.Sum();

        return this.liberties;
    }

    public void checkSurrounding()
    {
        foreach(var stone in this.stoneSides)
            if (stone != null)
                stone.checkLiberties(this.points);
    }

    public Vector2Int[] destroyStones(GameObject[,] points)
    {   
        Vector2Int[] destroyed = new Vector2Int[25];
        Vector2Int coords = new Vector2Int(0,0);
        int dCount = 0;


        if (this.checkLiberties(points)==0)
        {
            coords = new Vector2Int(this.x,this.y);
            destroyed[dCount] = coords;
            dCount++;
            Destroy(this.go);
            Debug.Log("destroyed stone");
        }
        

        foreach(var stone in this.stoneSides)
            if (stone != null)
                if (stone.checkLiberties(points)==0)
                {
                    coords = new Vector2Int(stone.x,stone.y);
                    destroyed[dCount] = coords;
                    dCount++;
                    Destroy(stone.go);
                    Debug.Log("destroyed stone");
                }
        return destroyed;
                

    }
}
