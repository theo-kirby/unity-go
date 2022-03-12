using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goban : MonoBehaviour
{
    private const int BOARD_SIZE = 19;
    private GameObject[,] points;
    private GameObject[] stones;
    public Camera currentCamera;
    private Vector2Int currentHover;
    public GameObject black_stone;
    public GameObject white_stone;
    public GameObject ghost_stone;
    public int counter = 0;

    

    private void Awake()
    {
        generatePoints(BOARD_SIZE);
    }
    
    private void Update()
    {
    RaycastHit info;
    Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out info, LayerMask.GetMask("point", "Hover")))
        {
            Vector2Int hitposition = getIntInd(info.transform.gameObject);

            if (Input.GetMouseButtonDown(0) && points[hitposition.x,hitposition.y].tag == "empty")
                {
                    playStone(0,hitposition.x,hitposition.y);
                }
            if (Input.GetMouseButtonDown(1) && points[hitposition.x,hitposition.y].tag == "empty")
                {
                    playStone(1,hitposition.x,hitposition.y);
                }

        }

        
        for (int x=0;x<BOARD_SIZE;x++)
            for (int y=0;y<BOARD_SIZE;y++)
                if (getLiberties(x,y) < 1)
                {
                    for (int s = 0; s < stones.Length; s++)
                    {
                        Vector3 pos = new Vector3(x,0,y);
                        if (stones[s].transform.position == pos)
                        Destroy(stones[s]);
                    }
                }
        
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

    point.layer = LayerMask.NameToLayer("point");
    point.AddComponent<BoxCollider>();
    mesh.RecalculateNormals();
    point.tag="empty";
    return point;
    }

    private void generatePoints(int boardSize)
    {
        points = new GameObject[boardSize,boardSize];
        for (int x=0;x<boardSize;x++)
            for (int y=0;y<boardSize;y++)
                points[x,y] = generateOnePoint(x,y);

    }

    private Vector2Int getIntInd(GameObject hitInfo)
    {
        for (int x=0; x< BOARD_SIZE; x++)
            for (int y=0; y< BOARD_SIZE; y++)
                if (points[x,y] == hitInfo)
                    return new Vector2Int(x,y);
        return -Vector2Int.one;
    }

    private void playStone(int color, int xindex, int yindex)
    {
        //GameObject stone = new GameObject(string.Format("S X:{0},Y:{1}",xindex,yindex));

        if (color == 0)
        {
            Vector3 temp = new Vector3(xindex,0,yindex);
            points[xindex,yindex].tag="black";
            GameObject bs = Instantiate(black_stone);
            bs.transform.position += temp;
            stones[counter] = bs;
            counter++;

        }

        if (color == 1)
        {
            Vector3 temp = new Vector3(xindex,0,yindex);
            points[xindex,yindex].tag="white";
            GameObject ws = Instantiate(white_stone);
            ws.transform.position += temp;
            stones[counter] = ws;
            counter++;
        }

        if (color == 2)
        {
            Vector3 temp = new Vector3(xindex,0,yindex);
            points[xindex,yindex].tag="ghost";
            GameObject gs = Instantiate(ghost_stone);
            gs.transform.position += temp;
            stones[counter] = gs;
            counter++;
        }
    }

    private int getLiberties(int x,int y)
    {

        GameObject right = new GameObject();
        GameObject left = new GameObject();
        GameObject up = new GameObject();
        GameObject down = new GameObject();
        int liberties = 0;
        
        points[x+1,y] = right;
        points[x-1,y] = left;
        points[x,y+1] = up;
        points[x,y-1] = down;

        if (right.tag == "empty")
            liberties++;
        if (left.tag == "empty")   
            liberties++;
        if (up.tag == "empty")
            liberties++;
        if (down.tag == "empty")
            liberties++;

        return liberties;
    }

    // private void removeStone(x,y)
    // {

    // }
}









    //public void Update()
    //{
    // RaycastHit info;
    // Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

    //         if(Physics.Raycast(ray, out info, LayerMask.GetMask("point", "Hover")))
    //         {
    //             //get index of vert
    //             Vector2Int hitposition = getIntInd(info.transform.gameObject);

    //             // if (points[hitposition.x,hitposition.y].tag == "empty")
    //             //     playStone(2, hitposition.x, hitposition.y);

    //             if(currentHover == -Vector2Int.one)
    //             {
    //                 currentHover = hitposition;
    //                 points[hitposition.x,hitposition.y].layer = LayerMask.NameToLayer("Hover");
                    
    //             }
    //             if (currentHover != hitposition)
    //             {
    //                 points[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("point");
    //                 currentHover = hitposition;
    //                 points[hitposition.x,hitposition.y].layer = LayerMask.NameToLayer("Hover");

    //             }

    //             if (Input.GetMouseButtonDown(0) && points[hitposition.x,hitposition.y].tag == "empty")
    //             {
    //                 playStone(1,hitposition.x,hitposition.y);
    //             }
    //             if (!Input.GetMouseButtonDown(0) && points[hitposition.x,hitposition.y].tag == "empty")
    //             {

    //                 int hpx = hitposition.x;
    //                 int hpy = hitposition.y;
    //                 playStone(2, hitposition.x, hitposition.y);
                    

    //                 if (hpx != hitposition.x)
    //                 {
    //                     removeStone(hpx, hpy);
    //                 }
    //             }
    //             else
    //             {
    //                 if(currentHover != -Vector2Int.one)
    //                 {
    //                     points[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("point");
    //                     currentHover = -Vector2Int.one;

    //                 }
    //             }                
    //         }   
    //     }

    //     RaycastHit info;
    //     Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);

    //     if(Physics.Raycast(ray, out info))
    //     {
    //         //get index of vert
    //         Vector2Int hitposition = getIntInd(info.transform.gameObject);

    //         playStone(0,hitposition.x,hitposition.y);
    //     }
    // }
        