using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Stone : MonoBehaviour
{
    public int x,y;

    public int north,east,south,west;

    public int liberties;
    public string color;

    public bool empty, dead;

    public GameObject gameObject;

    public Stone leftStone,rightStone,upStone,downStone;
    public Stone[] sides;
}

