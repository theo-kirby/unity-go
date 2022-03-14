using UnityEngine;

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

    public Stone[] sides;

    public int liberties = 0;
    

}
