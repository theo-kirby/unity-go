using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goban : MonoBehaviour
{
    private string empty = "empty";
    private string black = "black";
    private string white = "white";

    private const int BOARD_SIZE = 19;

    public Stone[,] stones;
    public GameObject[,] points;

    public Camera currentCamera;
    public Material[] colors;
    public GameObject go_stone;
    public GameObject no_stone;

    public Stone[] deadStones;

    private void Awake()
    {
        generateAllPoints(BOARD_SIZE);
        generateAllStones(BOARD_SIZE);
        deadStones = new Stone[50];
    }
    private void Update()
    {
        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out info))
        {
            Vector2Int hitposition = getPointIndex(info.transform.gameObject);

            if (Input.GetMouseButtonDown(0) && points[hitposition.x,hitposition.y].tag == empty)
                stones[hitposition.x,hitposition.y] = playStone(black,hitposition.x,hitposition.y);

            if (Input.GetMouseButtonDown(1) && points[hitposition.x,hitposition.y].tag == empty)
                stones[hitposition.x,hitposition.y] = playStone(white,hitposition.x,hitposition.y);
        }
    }
    private GameObject generateOnePoint(int x, int y)
    {
        GameObject point = new GameObject(string.Format("point#({0},{1})",x,y),typeof(BoxCollider));

        Vector3 temp = new Vector3(x,0,y);
        point.transform.position += temp;
        point.tag=empty;

        return point;
    }
    private Stone generateOneStone(int x, int y)
    {
        Stone stone = Instantiate(no_stone).GetComponent<Stone>();
        Vector3 temp = new Vector3(x,0,y);
        stone.transform.position += temp;
        stone.tag=empty;
        stone.empty = true;
        stone.dead = true;

        return stone;
    }
    private void generateAllPoints(int boardSize)
    {
        points = new GameObject[boardSize,boardSize];
        for (int x=0;x<boardSize;x++)
            for (int y=0;y<boardSize;y++)
                points[x,y] = generateOnePoint(x,y);
    }
    private void generateAllStones(int boardSize)
    {
        stones = new Stone[boardSize,boardSize];
        for (int x=0;x<boardSize;x++)
            for (int y=0;y<boardSize;y++)
                stones[x,y] = generateOneStone(x,y);
    }
    private Vector2Int getPointIndex(GameObject hitInfo)
    {
        for (int x=0; x< BOARD_SIZE; x++)
            for (int y=0; y< BOARD_SIZE; y++)
                if (points[x,y] == hitInfo)
                    return new Vector2Int(x,y);
        return -Vector2Int.one;
    }
    private Stone spawnStone(string color, int x, int y)
    {
        Stone stone = Instantiate(go_stone).GetComponent<Stone>();
        stone.color=color;stone.x=x;stone.y=y;
        int team;if(stone.color==black){team=0;}else{team=1;}
        stone.GetComponent<MeshRenderer>().material=colors[team];
        Vector3 temp = new Vector3(x,0,y);
        stone.transform.position += temp;
        return stone;
    }
    private Stone playStone(string color, int x, int y)
    {        
        Stone stone = spawnStone(color,x,y);

        stones[x,y] = stone;

        stone.setDirections();

        return stone;
    }
    private void getLibertiesOfAdjacent(Stone stone)
    {
        foreach (var side in stone.directions)
        {
            getLiberties(side);
        }
    }
    private bool getLiberties(Stone stone)
    {
        foreach (var l in stone.directions)

            if (stones[l].empty)
            {
                return true;
            }
            if (stones[l].color == stone.color)
            {
                foreach(var s in stones[l].directions)
                    getLiberties(s);
            }
        return false;
        
    }
            
        
}

