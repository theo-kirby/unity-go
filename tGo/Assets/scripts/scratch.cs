         // private bool isGroupDead(Stone stone)
    // {
    //     int dsc = 0;

    //     foreach (Stone side in stone.stoneSides)
    //     {
    //         if (side.empty == true)
    //         return false;

    //         if ((side.color == stone.color) && (side.dead == false))
    //             deadStones[dsc] = stone;
    //             dsc++;
    //             stone.dead=true;
    //             if (isGroupDead(side))
    //                 return true;
    //     }
    //     return true;
    // }

//     private void destroyGroup(Stone[] ds)
//     {
//         foreach (Stone stone in ds)
//         {
//             Destroy(stone.gameObject);
//         }
//     }
     
     
     
     
     
      // private void updateSurroundingStones(Stone stone)
    // {
    //     foreach (Stone s in stone.stoneSides)
    //         if (s.empty == false)
    //             updateStone(s);

    // }

        // foreach (Stone s in stone.stoneSides)
        // {
        //     Debug.Log(isGroupDead(s));
        //     if (!s.empty)
        //     {
        //         if (isGroupDead(s))
        //             destroyGroup(deadStones);
        //         else    
        //             deadStones = new Stone[50];
        //     }

        // }
        // if (isGroupDead(stone))
        //     foreach (Stone s in deadStones)
        //         destroyGroup(deadStones);

// using UnityEngine;
// using System.Collections.Generic;
// using System.Linq;
// using System;

// public class Stone : MonoBehaviour
// {
//     public string color;
//     public int x;
//     public int y;

//     public GameObject gameObject;

//     public int north = 0;
//     public int east = 0;
//     public int south = 0;
//     public int west = 0;

//     public Stone leftStone;
//     public Stone rightStone;
//     public Stone upStone;
//     public Stone downStone;

//     public Stone[] stoneSides;
//     public GameObject[] sides;
//     public GameObject[,] points;

//     public int[] libs = new int[4]{0,0,0,0};
//     public int liberties;
//     public int libCount;
//     public int gLiberties;

//     public Stone()
//     {
//         this.liberties = 0;
//         this.gLiberties = 0;
//     }

//     public int checkLiberties(GameObject[,] points)
//     {
//         this.libCount = 0;
//         this.points = points;
//         string empty = "empty";


//         Vector2Int coords = new Vector2Int(this.x,this.y);
//         GameObject l = points[this.x-1,this.y];
//         GameObject r = points[this.x+1,this.y];
//         GameObject u = points[this.x,this.y+1];
//         GameObject d = points[this.x,this.y-1];
//         GameObject[] sides = new GameObject[4]{l,r,u,d};

//         foreach (var side in sides)
//         {
//             if (side.tag == empty)
//             {
//                 this.libs[this.libCount] = 1;
//                 this.libCount++;
//             }
//             else if (side.tag != this.color)
//             {
//                 this.libs[this.libCount] = 0;
//                 this.libCount++;

//             }
//             else if (side.tag == this.color)
//             {
//                 this.libs[this.libCount] = 0;
//                 this.libCount++;
        
//             }
//         }
//         this.liberties = this.libs.Sum();

//         return this.liberties;
//     }

//     public void checkSurrounding()
//     {
//         foreach(var stone in this.stoneSides)
//             if (stone != null)
//                 stone.checkLiberties(this.points);
//     }

//     public Vector2Int[] destroyStones(GameObject[,] points)
//     {   
//         Vector2Int[] destroyed = new Vector2Int[25];
//         Vector2Int coords = new Vector2Int(0,0);
//         int dCount = 0;


//         if (this.checkLiberties(points)==0)
//         {
//             coords = new Vector2Int(this.x,this.y);
//             destroyed[dCount] = coords;
//             dCount++;
//             Destroy(this.go);
//             Debug.Log("destroyed stone");
//         }
        

//         foreach(var stone in this.stoneSides)
//             if (stone != null)
//                 if (stone.checkLiberties(points)==0)
//                 {
//                     coords = new Vector2Int(stone.x,stone.y);
//                     destroyed[dCount] = coords;
//                     dCount++;
//                     Destroy(stone.go);
//                     Debug.Log("destroyed stone");
//                 }
//         return destroyed;
//     }

//     public void checkGroup()
//     {

//     }

    // public GameObject makeGroup(GameObject allGroups)
    // {
    //     allGroups.AddComponent<Group>();
    //     allGroups.AddComponent<Group>();
    //     return allGroups;
    // }







//    using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class goban : MonoBehaviour
// {
//     private string empty = "empty";
//     private string black = "black";
//     private string white = "white";
//     private const int BOARD_SIZE = 19;

//     private Stone[,] stones = new Stone[BOARD_SIZE,BOARD_SIZE];
//     public GameObject[,] points;
//     private Group[] groups;

//     public Camera currentCamera;
//     public Material[] colors;
//     public GameObject go_stone;

//     public int groupCounter;
//     public GameObject allGroups;
//     public Groups g;
//     public Stone[] stoneArray;
//     public int stoneArrayCounter;


//     private void Awake()
//     {
//         generateAllPoints(BOARD_SIZE);
//         g = generateAllGroups();
//     }

//     private void Update()
//     {
//         RaycastHit info;
//         Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

//         if(Physics.Raycast(ray, out info))
//         {
//             Vector2Int hitposition = getPointIndex(info.transform.gameObject);

//             if (Input.GetMouseButtonDown(0) && points[hitposition.x,hitposition.y].tag == empty)
//                 stones[hitposition.x,hitposition.y] = playStone(black,hitposition.x,hitposition.y);

//             if (Input.GetMouseButtonDown(1) && points[hitposition.x,hitposition.y].tag == empty)
//                 stones[hitposition.x,hitposition.y] = playStone(white,hitposition.x,hitposition.y);
//         }
//     }

//     // Generate array points[x,y] of empty gameobjects with colliders
//     private void generateAllPoints(int boardSize)
//     {
//         points = new GameObject[boardSize,boardSize];
//         for (int x=0;x<boardSize;x++)
//             for (int y=0;y<boardSize;y++)
//                 points[x,y] = generateOnePoint(x,y);
//     }

//     private GameObject generateOnePoint(int x, int y)
//     {
//         GameObject point = new GameObject(string.Format("point#({0},{1})",x,y),typeof(BoxCollider));

//         Vector3 temp = new Vector3(x,0,y);
//         point.transform.position += temp;
//         point.tag=empty;

//         return point;
//     }

//     private Vector2Int getPointIndex(GameObject hitInfo)
//     {
//         for (int x=0; x< BOARD_SIZE; x++)
//             for (int y=0; y< BOARD_SIZE; y++)
//                 if (points[x,y] == hitInfo)
//                     return new Vector2Int(x,y);
//         return -Vector2Int.one;
//     }

//     private Stone playStone(string color, int x, int y)
//     {        
//         Stone stone = Instantiate(go_stone).GetComponent<Stone>();

//         stone.color=color;stone.x=x;stone.y=y;

//         int team;if(stone.color==black){team=0;}else{team=1;}

//         stone.GetComponent<MeshRenderer>().material=colors[team];

//         Vector3 temp = new Vector3(x,0,y);
//         stone.transform.position += temp;
//         points[x,y].tag = color;
//         stones[x,y] = stone;

//         stone.left = new Vector2Int(stone.x-1,stone.y);stone.right = new Vector2Int(stone.x+1,stone.y);stone.up = new Vector2Int(stone.x,stone.y+1);stone.down = new Vector2Int(stone.x,stone.y-1);
//         stone.leftStone = stones[stone.left.x,stone.left.y];stone.rightStone=stones[stone.right.x,stone.right.y];stone.upStone=stones[stone.up.x,stone.up.y];stone.downStone=stones[stone.down.x,stone.down.y];
//         stone.stoneSides = new Stone[4]{stone.leftStone,stone.rightStone,stone.upStone,stone.downStone};
        
//         stone.checkLiberties(points);
//         stone.checkSurrounding();
//         foreach (var dest in stone.destroyStones(points))
//             points[dest.x,dest.y].tag = empty;


//         Stone[] arr = new Stone[1];
//         arr[0] = stone;
//         g.addGroup(arr);
//         return stone;
//     }

//     public Groups generateAllGroups()
//     {
//         GameObject allGroups =  new GameObject("all_groups");
//         allGroups.AddComponent<Groups>();

//         Groups g = allGroups.GetComponent<Groups>();
//         g.stoneGroups = new Stone[25][];
//         for (int l=0;l<25;l++) 
//         {
//             g.stoneGroups[l] = new Stone[25];
//         }
//         return g;           
       
//     }
// }

// private void Awake()
//     {
//         groupCounter = 0;

//         GameObject allGroups =  new GameObject("all_groups");
//         allGroups.AddComponent<Groups>();
//         Groups g = allGroups.GetComponent<Groups>();
//         g.stoneGroups = new Stone[20][];

//         int c =0;
//         foreach (var arr in g.stoneGroups)
//         {
//             g.stoneGroups[c] = new Stone[20];
//             c++;
//         }
//         Stone[] stoneArray = new Stone[20];
//         Stone s1 = playStone(black,9,9);
//         s1 = stoneArray[stoneArrayCounter];
//         g.addGroup(stoneArray);

//         generateAllPoints(BOARD_SIZE);
//     }

//     private void Update()
//     {
//         RaycastHit info;
//         Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

//         if(Physics.Raycast(ray, out info))
//         {
//             Vector2Int hitposition = getPointIndex(info.transform.gameObject);

//             if (Input.GetMouseButtonDown(0) && points[hitposition.x,hitposition.y].tag == empty)
//                 stones[hitposition.x,hitposition.y] = playStone(black,hitposition.x,hitposition.y);

//             if (Input.GetMouseButtonDown(1) && points[hitposition.x,hitposition.y].tag == empty)
//                 stones[hitposition.x,hitposition.y] = playStone(white,hitposition.x,hitposition.y);
//         }
//     }

                
                // foreach (var st in this.stoneSides)
                //     if (st != null)
                //         if (st.color == this.color)
                //             if (st.x == side.transform.position.x && st.y == side.transform.position.y)
                //                 this.gLiberties = st.liberties;
                                
   
   
   
   
   
   
   
    // public int checkLiberties()
    // {
    //     this.libCount = 0;
    //     string sameTeam = this.color;

    //     foreach (var stone in this.sides)
    //     {
    //         if (stone == null)
    //         {
    //         this.libs[this.libCount] = 1;
    //         this.libCount++;
    //         }
    //         else if (stone.color == sameTeam)
    //         {
    //             this.libs[this.libCount] = 0;
    //             this.libCount++;
    //              //add both to group
    //         }
    //     }
    //     this.liberties = this.libs.Sum();
    //     return this.liberties;
    // }

    // public void updateLiberties()
    // {
    //     foreach (var s in this.sides)
    //         if (s != null)
    //             s.checkLiberties();
    // }


   // private bool checkGroup(Stone stone)
    // {
    //     string sameTeam = stone.color;

    //     //is this playing position putting stone in existing group?
    //     foreach (Stone side in stone.sides)
    //         if (side != null)
    //             if (side.color == sameTeam)
    //             {
    //                 addToGroup(stone, side.go.GetComponent<Group>());
    //                 return true;
    //             }
    //     return false;
    // }

    // private int checkGroupLiberties(Group group)
    // {
    //     foreach (var stone in group.stonesInGroup)
    //     {
    //         checkStoneLiberties(stone);
    //         //group.liberties = (stone.ll+stone.rl+stone.ul+stone.dl);
    //     }
    //     return group.liberties;
    // }

//     private void addNewGroup(Stone stone)
//     {
//             int stonecounter = 0;

//             stone.go.AddComponent<Group>();
//             Group group = stone.go.GetComponent<Group>();

//             group.color = stone.color;
//             group.ID = groupCounter;
//             groupCounter++;

//             group.stonesInGroup = new Stone[10];
//             group.stonesInGroup[stonecounter] = stone;
//             group.stoneCount++;
//     }

//     private Group addToGroup(Stone stone, Group group)
//     {
//         group.stonesInGroup[group.stoneCount] = stone;
//         return group;
//     }

// }

//private void checkStoneLiberties(Stone stone)
    // {
    //     if (stone != null)
    //     {
    //         foreach (Stone ston in stone.sides)
    //         {
    //             string sameTeam = stone.color;

    //             if(ston == null)
    //             {
    //                 if (ston == stone.leftStone)
    //                     stone.ll = 1;
    //                 if (ston == stone.rightStone)
    //                     stone.rl = 1;
    //                 if (ston == stone.upStone)
    //                     stone.ul = 1;
    //                 if (ston == stone.downStone)
    //                     stone.dl = 1;
    //             }
    //             else if (ston.color == sameTeam)
    //             {
    //                 addToGroup(ston, stone.GetComponent<Group>());
    //                 if (ston == stone.leftStone)
    //                     stone.ll = 0;
    //                 if (ston == stone.rightStone)
    //                     stone.rl = 0;
    //                 if (ston == stone.upStone)
    //                     stone.ul = 0;
    //                 if (ston == stone.downStone)
    //                     stone.dl = 0;
    //             }
    //             else
    //             {
    //                 if (ston == stone.leftStone)
    //                     stone.ll = 0;
    //                 if (ston == stone.rightStone)
    //                     stone.rl = 0;
    //                 if (ston == stone.upStone)
    //                     stone.ul = 0;
    //                 if (ston == stone.downStone)
    //                     stone.dl = 0;
    //             }
    //             stone.liberties = (stone.ll+stone.rl+stone.ul+stone.dl);
    //         }
    //     }
    // }



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



          //checkGroupLiberties(stone.go.GetComponent<Group>());
        // if (stone.rightStone != null)
        //     checkStoneLiberties(stone.rightStone);
        // if (stone.leftStone != null)
        //     checkStoneLiberties(stone.leftStone);
        // if (stone.upStone != null)
        //     checkStoneLiberties(stone.upStone);
        // if (stone.downStone != null)
        //     checkStoneLiberties(stone.downStone);
        // if (stone.rightStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.rightStone.go.GetComponent<Group>());
        // if (stone.leftStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.leftStone.go.GetComponent<Group>());
        // if (stone.upStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.upStone.go.GetComponent<Group>());
        // if (stone.downStone.go.GetComponent<Group>() != null)
        //     checkGroupLiberties(stone.downStone.go.GetComponent<Group>());
