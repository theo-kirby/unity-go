using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goban : MonoBehaviour
{

    private const int BOARD_SIZE = 19;

    private Stone[,] stones = new Stone[BOARD_SIZE,BOARD_SIZE];

    private GameObject[,] points;

    public Camera currentCamera;
    public Material[] colors;
    public GameObject go_stone;
    public int groupCounter;
    public GameObject[] groups;


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

        // for (int i=0;i<stones.GetLength(0);i++)
        //     for (int l=0;l<stones.GetLength(1);l++)
        //         if (stones[i,l] != null)
        //             if (getLiberties(stones[i,l]) == 0)
        //                 Destroy(stones[i,l]);


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
        stone.sides = new Stone[4]{stone.leftStone,stone.rightStone,stone.upStone,stone.downStone};
        addNewGroup(stone);
        return stone;
    }

    private void checkStoneLiberties(Stone stone)
    {
        foreach (var ston in stone.sides)
        {
            string sameTeam = stone.color;

            if(ston == null)
            {
                if (ston == stone.leftStone)
                    stone.ll = 1;
                if (ston == stone.rightStone)
                    stone.rl = 1;
                if (ston == stone.upStone)
                    stone.ul = 1;
                if (ston == stone.downStone)
                    stone.dl = 1;
            }
            else if (ston.color == sameTeam)
            {
                addToGroup(stone, ston.GetComponent<Group>());
                if (ston == stone.leftStone)
                    stone.ll = 0;
                if (ston == stone.rightStone)
                    stone.rl = 0;
                if (ston == stone.upStone)
                    stone.ul = 0;
                if (ston == stone.downStone)
                    stone.dl = 0;
            }
            else
            {
                if (ston == stone.leftStone)
                    stone.ll = 0;
                if (ston == stone.rightStone)
                    stone.rl = 0;
                if (ston == stone.upStone)
                    stone.ul = 0;
                if (ston == stone.downStone)
                    stone.dl = 0;
            }
        }
    }
    

    private void addNewGroup(Stone stone)
    {
        if (stone.go.GetComponent<Group>() == null)
        {
            int stonecounter = 0;
            stone.go.AddComponent<Group>();
            Group group = stone.go.GetComponent<Group>();
            group.color = stone.color;
            group.groupNumber = groupCounter;
            groupCounter++;
            group.stonesInGroup = new Stone[10];
            group.stonesInGroup[stonecounter] = stone;
            group.stoneCount++;
        }

    }

    private Group addToGroup(Stone stone, Group group)
    {
        return group;
    }

}













//     {
//         int groupcount = 0;
//         int count = 0;
//         string sameTeam = stone.color;

//         stone.ll=0;
//         stone.rl=0;
//         stone.ul=0;
//         stone.dl=0;



//         // Stone leftstone, rightstone, upstone, downstone;
        
//         // leftstone = stones[stone.left.x,stone.left.y];
//         // rightstone = stones[stone.right.x,stone.right.y];
//         // upstone = stones[stone.up.x,stone.up.y];
//         // downstone = stones[stone.down.x,stone.down.y];

//         if (stones[stone.left.x,stone.left.y] == null)
//             stone.ll = 1;

//         else if (stones[stone.left.x,stone.left.y].color == sameTeam)
//         {
//             //new group
//             GameObject groEmpty = new GameObject(string.Format("group {0}",groupcount ));
//             groupcount++;
//             //assign and get component group script
//             groEmpty.AddComponent<Group>();
//             Group groComp = groEmpty.GetComponent<Group>();
//             //add both stones to group;
//             groComp.stonesInGroup = new Stone[25];
//             groComp.stonesInGroup[count] = stone;
//             count++;
//             groComp.stonesInGroup[count] = stones[stone.left.x,stone.left.y];

//         }

//         else if (stones[stone.left.x,stone.left.y].color != sameTeam)
//             stone.ll = 0;

//         stone.liberties = (stone.ll);

//     }
//     private void checkGroupLiberties(Group group)
//     {
//         foreach (var ston in group.stonesInGroup)
//             Debug.Log(ston);
//     }

    
// }




        // stone.left = new Vector2Int(stone.x-1,stone.y);
        // stone.right = new Vector2Int(stone.x+1,stone.y);
        // stone.up = new Vector2Int(stone.x,stone.y+1);
        // stone.down = new Vector2Int(stone.x,stone.y-1);

// private int getLiberties(Stone stone)
//     {
//         for (int l=0;l<stone.spaces.Length;l++)
//         {
//             if (stones[stone.spaces[l].x,stone.spaces[l].y] == null)
//                 stone.liberties++;
//         }
//         return stone.liberties;
//     }

//     private void assignLiberties(Stone stone)
//     {
//         int counter = 0;

//         stone.spaces[counter] = stone.left;
//         counter++;
//         stone.spaces[counter] = stone.right;
//         counter++;
//         stone.spaces[counter] = stone.up;
//         counter++;
//         stone.spaces[counter] = stone.down;
//         counter++;
//     }

        //point.layer = LayerMask.NameToLayer("point");
        //point.tag="empty";

        //GameObject stone = new GameObject(string.Format("S X:{0},Y:{1}",xindex,yindex));
    
            // Vector3 temp = new Vector3(xindex,0,yindex);
            // points[xindex,yindex].tag="black";
            // GameObject bs = Instantiate(black_stone);
            // bs.transform.position += temp;
            // stones[counter] = bs;
            // counter++;

    

    //     private GameObject generateOnePoint(int x, int y)
    // {
    //  GameObject point = new GameObject(string.Format("point#({0},{1})",x,y));
    // //  point.transform.parent = transform;

    // //  Mesh mesh = new Mesh();
    // //  point.AddComponent<MeshFilter>().mesh= mesh;
    // //  point.AddComponent<MeshRenderer>();

    //  Vector3 temp = new Vector3(x,0,y);
    //  point.transform.position += temp;

    // // point.layer = LayerMask.NameToLayer("point");
    // point.AddComponent<BoxCollider>();
    // //mesh.RecalculateNormals();
    // // point.tag="empty";
    // return point;
    // }

        // var connections = new List<Stone>();

        // connections.Add(leftstone);
        // connections.Add(rightstone);
        // connections.Add(upstone);
        // connections.Add(downstone);

        // foreach (var connection in connections)
        // {
        //     if (connection == null)
        //         stone.liberties++;
        // }

        // if (stone.liberties == 0)
        //     Destroy(stone.go);
        // {
        //     stone.ll = 0;
        //     GameObject stonegroup = new GameObject("group{groupcount}");
        //     groupcount++;
        //     stonegroup.AddComponent<Group>();
        //     Group g = stonegroup.GetComponent<Group>();
        //     g.stonesInGroup = new Stone[10];
        //     g.stonesInGroup[count] = stone;
        //     count++;
        //     g.stonesInGroup[count] = stones[stone.left.x,stone.left.y];

                // else if (stones[stone.right.x,stone.right.y] == null)
        //     stone.rl =1;
        // else if (stones[stone.up.x,stone.up.y] == null)
        //     stone.ul = 1;
        // else if (stones[stone.down.x,stone.down.y] == null)
        //     stone.dl =1;



                // Stone leftstone, rightstone, upstone, downstone;

        // leftstone = stones[stone.left.x,stone.left.y];
        // rightstone = stones[stone.right.x,stone.right.y];
        // upstone = stones[stone.up.x,stone.up.y];
        // downstone = stones[stone.down.x,stone.down.y];

        // if (leftstone != null)
        //     checkStoneLiberties(leftstone);
        // if (rightstone != null)
        //     checkStoneLiberties(rightstone);
        // if (upstone != null)
        //     checkStoneLiberties(upstone);
        // if (downstone != null)
        //     checkStoneLiberties(downstone);

//int lone, ltwo, lthree, lfour;
    //     foreach (var ston in stone.sides)
    //     {
    //         if(ston == null)
    //         {

    //         }
    //         else if (ston.team == sameTeam)
    //         {

    //         }
    //         else
    //         {

    //         }
    //     }
    // }