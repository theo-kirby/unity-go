using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goban : MonoBehaviour
{

    private const int BOARD_SIZE = 19;

    private Stone[,] stones = new Stone[BOARD_SIZE,BOARD_SIZE];
    public GameObject[,] points;


    public Camera currentCamera;
    public Material[] colors;
    public GameObject go_stone;

    private string empty = "empty";
    private string black = "black";
    private string white = "white";



    private void Awake()
    {
        generateAllPoints(BOARD_SIZE);
        //playStone(black,9,9);
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
        GameObject point = new GameObject(string.Format("points[{0},{1}]",x,y),typeof(BoxCollider));
        point.AddComponent<Point>();
        Vector3 temp = new Vector3(x,0,y);
        point.transform.position += temp;
        point.tag=empty;

        return point;
    }

    private void generateAllPoints(int boardSize)
    {
        points = new GameObject[boardSize,boardSize];
        for (int x=0;x<boardSize;x++)
            for (int y=0;y<boardSize;y++)
                points[x,y] = generateOnePoint(x,y);
    }


    private Vector2Int getPointIndex(GameObject hitInfo)
    {
        for (int x=0; x< BOARD_SIZE; x++)
            for (int y=0; y< BOARD_SIZE; y++)
                if (points[x,y] == hitInfo)
                    return new Vector2Int(x,y);
        return -Vector2Int.one;
    }

    // private void scan(int x, int y)
    // {
    //     string color = points[x,y].tag;
    //     Debug.Log(color);
    //     for (int i=0; i++; i<4)
    //         if (points)
    // }

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
        Point p = points[x,y].GetComponent<Point>();
        p.stone = stone;
        stone.transform.SetParent(points[x,y].transform);

        setConnections(points[x,y]);
        killSurrounding(points[x,y]);


        return stone;
    }

    public void setConnections(GameObject point)
    {
        Stone stone = point.transform.GetChild(0).GetComponent<Stone>();
        int x = stone.x;
        float y = stone.y;
        string team = point.tag;

        List<GameObject> neighbors = new List<GameObject>();

        GameObject north = points[(int)x,(int)y+1];
        neighbors.Add(north);
        GameObject east = points[(int)x+1,(int)y];
        neighbors.Add(east);
        GameObject south = points[(int)x,(int)y-1];
        neighbors.Add(south);
        GameObject west = points[(int)x-1,(int)y];
        neighbors.Add(west);

        foreach (GameObject n in neighbors)
        {
            if (n.tag == team)
            {
                if (!point.GetComponent<Point>().connections.Contains(n))
                    point.GetComponent<Point>().connections.Add(n);
                if (!n.GetComponent<Point>().connections.Contains(point))
                    n.GetComponent<Point>().connections.Add(point);
            }
        }
    }

    public GameObject killSurrounding(GameObject point)
    {
        Stone stone = point.transform.GetChild(0).GetComponent<Stone>();
        int x = stone.x;
        float y = stone.y;
        string team = point.tag;

        List<GameObject> neighbors = new List<GameObject>();

        GameObject north = points[(int)x,(int)y+1];
        neighbors.Add(north);
        GameObject east = points[(int)x+1,(int)y];
        neighbors.Add(east);
        GameObject south = points[(int)x,(int)y-1];
        neighbors.Add(south);
        GameObject west = points[(int)x-1,(int)y];
        neighbors.Add(west);

        foreach (GameObject n in neighbors)
        {
            Point p = n.GetComponent<Point>();

            if (n.tag == empty)
                break;
            else if (n.tag == team)
                killSurrounding(n);
            else
                foreach (GameObject connection in p.connections) 
                {
                    if (connection.tag == "empty")
                        return connection;
                    
                    else if (connection.tag != n.tag)
                        break;

                    else if (connection.tag == n.tag)
                        killSurrounding(connection);
                }
            destroyConnections(n);                
        }
        return null;
    }

    public void destroyConnections(GameObject point)
    {
        
        Point p = point.GetComponent<Point>();

        foreach (GameObject connection in p.connections)
            Destroy(connection.transform.GetChild(0).gameObject);
        //Destroy(stone);
        //Destroy(point.transform.GetChild(0));
    }

}


// private List<GameObject> getConnected(Stone stone)
//     {
//         List<GameObject> connections = new List<GameObject>();
//         Stone firstStone = stone;

//         if ((points[firstStone.x,firstStone.y+1].tag == firstStone.color) && (!connections.Contains(points[firstStone.x,firstStone.y+1])))
//             connections.Add(points[firstStone.x,firstStone.y+1]);
//         if (points[firstStone.x+1,firstStone.y].tag == firstStone.color && !connections.Contains(points[firstStone.x+1,firstStone.y]))
//             connections.Add(points[firstStone.x+1,firstStone.y]);
//         if (points[firstStone.x,firstStone.y-1].tag == firstStone.color && !connections.Contains(points[firstStone.x,firstStone.y-1]))
//             connections.Add(points[firstStone.x,firstStone.y-1]);
//         if (points[firstStone.x-1,firstStone.y].tag == firstStone.color && !connections.Contains(points[firstStone.x-1,firstStone.y]))
//             connections.Add(points[firstStone.x-1,firstStone.y]);

//         firstStone.connected = connections;
//         // getConnected(stones[firstStone.x,firstStone.y+1]);
//         // getConnected(stones[firstStone.x+1,firstStone.y]);
//         // getConnected(stones[firstStone.x,firstStone.y-1]);
//         // getConnected(stones[firstStone.x-1,firstStone.y]);

//         return connections;
        
//     }


//     private void scan(int c, int u)
//     {
//         private string color = points[c,u].tag;
//         //Debug.Log(color);
//     }
// }