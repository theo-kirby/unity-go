using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goban : MonoBehaviour
{

    private const int BOARD_SIZE = 19;
    private GameObject[,] points;


    private void Awake()
    {
        generatePoints(BOARD_SIZE);


    }

    private void generatePoints(int boardSize)
    {
        points = new GameObject[boardSize,boardSize];
        for (int x=0;x<boardSize;x++)
            for (int y=0;y<boardSize;y++)
                points[x,y] = generateOnePoint(x,y);

    }


    private GameObject generateOnePoint(int x, int y)
    {
     GameObject point = new GameObject(string.Format("X:{0},Y:{1}",x,y));
     point.transform.parent = transform;

     Mesh mesh = new Mesh();
     point.AddComponent<MeshFilter>().mesh= mesh;
     point.AddComponent<MeshRenderer>();

     Vector3 temp = new Vector3(x,0,y);
     point.transform.position += temp;

    //  Vector3[] vertices = new Vector3[1];
    //  vertices[0]= new Vector3(x,0,y);
    //  int[] tris = new int[] {0};
    //  mesh.vertices = vertices;
     point.AddComponent<BoxCollider>();

     return point;
    }
}
