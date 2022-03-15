using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goban : MonoBehaviour
{

    private const int BOARD_SIZE = 19;

    private Stone[,] stones = new Stone[BOARD_SIZE,BOARD_SIZE];
    public GameObject[,] points;
    private Group[] groups;


    public Camera currentCamera;
    public Material[] colors;
    public GameObject go_stone;
    public int groupCounter;

    private string empty = "empty";
    private string black = "black";
    private string white = "white";

    private List<Stone> deadStones = new List<Stone>();


    private void Awake()
    {
        groupCounter = 0;
        generateAllPoints(BOARD_SIZE);
        playStone(black,9,9);
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

    // Generate array points[x,y] of empty gameobjects with colliders
    private void generateAllPoints(int boardSize)
    {
        points = new GameObject[boardSize,boardSize];
        for (int x=0;x<boardSize;x++)
            for (int y=0;y<boardSize;y++)
                points[x,y] = generateOnePoint(x,y);
    }

    private GameObject generateOnePoint(int x, int y)
    {
        GameObject point = new GameObject(string.Format("point#({0},{1})",x,y),typeof(BoxCollider));

        Vector3 temp = new Vector3(x,0,y);
        point.transform.position += temp;
        point.tag=empty;

        return point;
    }

    private Vector2Int getPointIndex(GameObject hitInfo)
    {
        for (int x=0; x< BOARD_SIZE; x++)
            for (int y=0; y< BOARD_SIZE; y++)
                if (points[x,y] == hitInfo)
                    return new Vector2Int(x,y);
        return -Vector2Int.one;
    }

    private Stone playStone(string color, int x, int y)
    {        
        Stone stone = Instantiate(go_stone).GetComponent<Stone>();

        stone.color=color;stone.x=x;stone.y=y;

        int team;if(stone.color==black){team=0;}else{team=1;}

        stone.GetComponent<MeshRenderer>().material=colors[team];

        Vector3 temp = new Vector3(x,0,y);
        stone.transform.position += temp;
        points[x,y].tag = color;
        stones[x,y] = stone;

        stone.left = new Vector2Int(stone.x-1,stone.y);stone.right = new Vector2Int(stone.x+1,stone.y);stone.up = new Vector2Int(stone.x,stone.y+1);stone.down = new Vector2Int(stone.x,stone.y-1);
        stone.leftStone = stones[stone.left.x,stone.left.y];stone.rightStone=stones[stone.right.x,stone.right.y];stone.upStone=stones[stone.up.x,stone.up.y];stone.downStone=stones[stone.down.x,stone.down.y];
        stone.stoneSides = new Stone[4]{stone.leftStone,stone.rightStone,stone.upStone,stone.downStone};
        
        stone.checkLiberties(points);
        stone.checkSurrounding();
        foreach (var dest in stone.destroyStones(points))
            points[dest.x,dest.y].tag = empty;


        return stone;
    }
}
