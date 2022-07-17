// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class game : MonoBehaviour
// {
//     private string empty = "empty";
//     private string black = "black";
//     private string white = "white";
//     private string green = "green";
//     private string red = "red";

//     private const int BOARD_SIZE = 19;

//     public Stone[,] stones;
//     public GameObject[,] points;

//     public Camera currentCamera;
//     public Material[] colors;
//     public GameObject go_stone;
//     public GameObject no_stone;

//     public Stone[] deadStones;

//     private void Awake()
//     {
//         generateIntersections(BOARD_SIZE);
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
//     private GameObject generateIntersection(int x, int y)
//     {
//         GameObject point = new GameObject(string.Format("point#({0},{1})",x,y),typeof(BoxCollider));

//         Vector3 temp = new Vector3(x,0,y);
//         point.transform.position += temp;
//         point.tag=empty;

//         return point;
//     }

//     private void generateIntersections(int boardSize)
//     {
//         points = new GameObject[boardSize,boardSize];
//         for (int x=0;x<boardSize;x++)
//             for (int y=0;y<boardSize;y++)
//                 points[x,y] = generateOnePoint(x,y);
//     }
    
//     private Vector2Int getPointIndex(GameObject hitInfo)
//     {
//         for (int x=0; x< BOARD_SIZE; x++)
//             for (int y=0; y< BOARD_SIZE; y++)
//                 if (points[x,y] == hitInfo)
//                     return new Vector2Int(x,y);
//         return -Vector2Int.one;
//     }


    
// }
