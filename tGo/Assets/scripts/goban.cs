using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



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

    private Stone playStone(string color, int x, int y)
    {        
        Stone stone = Instantiate(go_stone).GetComponent<Stone>();

        stone.color=color;stone.x=x;stone.y=y;

        int team;if(stone.color==black){team=0;}else{team=1;}

        stone.GetComponent<MeshRenderer>().material=colors[team];

        Vector3 temp = new Vector3(x,0,y);
        stone.transform.position += temp;
        //Debug.Log("Played stone at ("+x+","+y+")");
        points[x,y].tag = color;
        stones[x,y] = stone;
        Point p = points[x,y].GetComponent<Point>();
        p.x=stone.x;p.y=stone.y;p.stone=stone;
        stone.transform.SetParent(points[x,y].transform);

        setConnections(p);
        getGroup(p);
        sendGroup(p);
        getNeighbors(p);
        getReach(p);
        sendReach(p);
        validGroup(p);

        return stone;
    }

    public void setConnections(Point point)
    {
        Stone stone = point.gameObject.transform.GetChild(0).GetComponent<Stone>();
        int x = stone.x;
        int y = stone.y;
        string team = point.gameObject.tag;

        List<Point> neighbors = new List<Point>();

        if (y+1 >= 0 && y+1 <= 18)
        {Point north = points[(int)x,(int)y+1].GetComponent<Point>();neighbors.Add(north);}
        if (x+1 >= 0 && x+1 <= 18)
        {Point east = points[(int)x+1,(int)y].GetComponent<Point>();neighbors.Add(east);}
        if (y-1 >= 0 && y-1 <= 18)
        {Point south = points[(int)x,(int)y-1].GetComponent<Point>();neighbors.Add(south);}
        if (x-1 >= 0 && x-1 <= 18)
        {Point west = points[(int)x-1,(int)y].GetComponent<Point>();neighbors.Add(west);}

        foreach (Point n in neighbors)
        {
            GameObject go = n.gameObject;
            if (go.tag == team)
            {
                if (!point.GetComponent<Point>().connections.Contains(go.GetComponent<Point>()))
                    point.GetComponent<Point>().connections.Add(go.GetComponent<Point>());
                if (!go.GetComponent<Point>().connections.Contains(point.GetComponent<Point>()))
                    go.GetComponent<Point>().connections.Add(point.GetComponent<Point>());
            }
        }
    }

    public HashSet<Point> getGroup(Point point)
    {
        HashSet<Point> visited = new HashSet<Point>();
        
        if (point.connections.Contains(point))
                return visited;
            
        var queue = new Queue<Point>();
        queue.Enqueue(point);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();

            if (visited.Contains(vertex))
                {continue;}

            visited.Add(vertex);
            if (!point.group.Contains(vertex))
                {
                    point.group.Add(vertex);
                    point.dupGroup.Add(vertex);
                }

            foreach(Point neighbor in vertex.connections)
                if (!visited.Contains(neighbor))
                    queue.Enqueue(neighbor);
        }

        return visited;
    }

    public void sendGroup(Point point)
    {
        foreach (Point member in point.group)
        {
            foreach (Point p in point.group)
            if (!member.group.Contains(p))
            {
               member.group.Add(p);
               member.dupGroup.Add(p);
            }
        }
    }

    public void getNeighbors(Point point)
    {
        if (point.y+1 < 19)
        {
            Point north = points[(int)point.x,(int)point.y+1].GetComponent<Point>();
            point.neighbors.Add(north);
        }
        if (point.x+1 < 19)
        {
            Point east = points[(int)point.x+1,(int)point.y].GetComponent<Point>();
            point.neighbors.Add(east);
        }
        if (point.y-1 > -1)
        {
            Point south = points[(int)point.x,(int)point.y-1].GetComponent<Point>();
            point.neighbors.Add(south);
        }
        if (point.x-1 > -1)
        {
            Point west = points[(int)point.x-1,(int)point.y].GetComponent<Point>();
            point.neighbors.Add(west);
        }
    }    

    public void getReach(Point point)
    {
        foreach (Point member in point.group)
            foreach( Point neighbor in member.neighbors)
                if (!point.reached.Contains(neighbor))
                    point.reached.Add(neighbor);
    }

    public void sendReach(Point point)
    {
        foreach (Point member in point.group)
            foreach (Point p in point.reached)
                if (!member.reached.Contains(p))
                    member.reached.Add(p);
    }

    public void validGroup(Point point)
    {
        bool libertyFound = false;
        foreach (Point reach in point.reached)
            if (reach.gameObject.tag == empty)
                libertyFound = true;
        if (libertyFound)
            Debug.Log("true");
        else
            Debug.Log("false");
        
    }

}


    //    public void getReach(Point point)
    // {
    //     foreach (Point member in point.group)
    //     {
    //         List<Point> neighbors = new List<Point>();
    //         int x = (int)member.gameObject.transform.position.x;
    //         int y = (int)member.gameObject.transform.position.x;

    //         if (y+1 <= 18)
    //         {Point north = points[(int)x,(int)y+1].GetComponent<Point>();
    //         neighbors.Add(north);}
    //         if (x+1 <= 18)
    //         {Point east = points[(int)x+1,(int)y].GetComponent<Point>();
    //         neighbors.Add(east);}
    //         if (y-1 >= 0)
    //         {Point south = points[(int)x,(int)y-1].GetComponent<Point>();
    //         neighbors.Add(south);}
    //         if (x-1 >= 0)
    //         {Point west = points[(int)x-1,(int)y].GetComponent<Point>();
    //         neighbors.Add(west);}

    //         foreach(Point neighbor in neighbors)
    //             if (!point.reached.Contains(neighbor))
    //                 point.reached.Add(neighbor);

    //     }
        
    // }
    //public bool isGroupValid(Point point)
    // {
    //     List<Point> reached = new List<Point>();
    //     reached.Add(point);

    //     foreach (Point p in point.group)
    //     {
    //         List<Point> neighbors = new List<Point>();
    //         int x = (int)point.gameObject.transform.position.x;
    //         int y = (int)point.gameObject.transform.position.x;

    //         if (y+1 <= 18)
    //         {Point north = points[(int)x,(int)y+1].GetComponent<Point>();
    //         neighbors.Add(north);}
    //         if (x+1 <= 18)
    //         {Point east = points[(int)x+1,(int)y].GetComponent<Point>();
    //         neighbors.Add(east);}
    //         if (y-1 >= 0)
    //         {Point south = points[(int)x,(int)y-1].GetComponent<Point>();
    //         neighbors.Add(south);}
    //         if (x-1 >= 0)
    //         {Point west = points[(int)x-1,(int)y].GetComponent<Point>();
    //         neighbors.Add(west);}
        
    //         foreach(Point n in neighbors)
    //             reached.Add(n);
    //     }

    //     foreach (Point r in reached)
    //         if (r.gameObject.tag == empty)
    //             return true;
                
    //     return false;
    // }
//     public bool isGroupValid(Point point)
//     {
//         int x = (int)point.gameObject.transform.position.x;
//         int y = (int)point.gameObject.transform.position.x;
//         string team = point.gameObject.tag;
//         string opponent = "";
//         bool libertyFound = false;
//         List<Point> neighbors = new List<Point>();

//         if (team == white)
//             opponent = black;
//         else if (team == black)
//             opponent = white;
        
//         if (y+1 >= 0 && y+1 <= 18)
//         {Point north = points[(int)x,(int)y+1].GetComponent<Point>();
//         neighbors.Add(north);}
//         if (x+1 >= 0 && x+1 <= 18)
//         {Point east = points[(int)x+1,(int)y].GetComponent<Point>();
//         neighbors.Add(east);}
//         if (y-1 >= 0 && y-1 <= 18)
//         {Point south = points[(int)x,(int)y-1].GetComponent<Point>();
//         neighbors.Add(south);}
//         if (x-1 >= 0 && x-1 <= 18)
//         {Point west = points[(int)x-1,(int)y].GetComponent<Point>();
//         neighbors.Add(west);}

//         if (point.dupGroup.Count == 0)
//             foreach (Point neighbor in neighbors)
//             {
//                 if (neighbor.gameObject.tag == empty)
//                     libertyFound = true;
//             }

//         else if (point.dupGroup.Count > 0)
//         {
//             List<Point> dg = new List<Point>(point.dupGroup);
//             foreach (Point p in dg)
//             {
//                 p.dupGroup.Remove(point);
//                 isGroupValid(p);
//             }
//         }
        
//         if (libertyFound)
//             return true;
//         else
//             return false;

//     }
// }







 // public bool validGroup(List<Point> group)
    // {
    //     bool libertyFound = false;
    //     foreach (Point p in group)
    //     {
        
        
    //     int x = (int)p.gameObject.transform.position.x;
    //     int y = (int)p.gameObject.transform.position.y;

    //     List<Point> neighbors = new List<Point>();

    //     if (y+1 >= 0 && y+1 <= 18)
    //     {Point north = points[(int)x,(int)y+1].GetComponent<Point>();neighbors.Add(north);}
    //     if (x+1 >= 0 && x+1 <= 18)
    //     {Point east = points[(int)x+1,(int)y].GetComponent<Point>();neighbors.Add(east);}
    //     if (y-1 >= 0 && y-1 <= 18)
    //     {Point south = points[(int)x,(int)y-1].GetComponent<Point>();neighbors.Add(south);}
    //     if (x-1 >= 0 && x-1 <= 18)
    //     {Point west = points[(int)x-1,(int)y].GetComponent<Point>();neighbors.Add(west);}
        
        
    //     while (!libertyFound)
    //     {
    //         foreach (Point neighbor in neighbors)
    //         {
    //             if (neighbor.gameObject.tag == empty)
    //             {
    //                 Debug.Log(neighbor.gameObject.name);
    //                 libertyFound = true;
    //             }
    //             else if (neighbor.gameObject.tag == black)
    //                 continue;
    //             else if (neighbor.gameObject.tag == white)
    //                 continue;
    //         }
    //     }
        
    //     }
    // if (libertyFound)
    //     return true;
    // else
    //     return false;
    // }










    //     foreach (Point neighbor in neighbors)
    //     {
    //         GameObject go = neighbor.gameObject;
    //         if (go.tag == empty)
    //         {
    //             Debug.Log(go.name);
    //             return true;     
    //         }       
    //     }
    //     }
    // return false;
    


// public GameObject killSurrounding(GameObject point)
//     {
//         Stone stone = point.transform.GetChild(0).GetComponent<Stone>();
//         int x = stone.x;
//         float y = stone.y;
//         string team = point.tag;

//         List<GameObject> neighbors = new List<GameObject>();

//         if (y+1 >= 0 && y+1 <= 18)
//         {GameObject north = points[(int)x,(int)y+1];neighbors.Add(north);}
//         if (x+1 >= 0 && x+1 <= 18)
//         {GameObject east = points[(int)x+1,(int)y];neighbors.Add(east);}
//         if (y-1 >= 0 && y-1 <= 18)
//         {GameObject south = points[(int)x,(int)y-1];neighbors.Add(south);}
//         if (x-1 >= 0 && x-1 <= 18)
//         {GameObject west = points[(int)x-1,(int)y];neighbors.Add(west);}

//         foreach (GameObject n in neighbors)
//         {
//             Point p = n.GetComponent<Point>();

//             if (n.tag == empty)
//                 break;
//             else if (n.tag == team)
//                 killSurrounding(n);
//             else
//                 foreach (GameObject connection in p.connections) 
//                 {
//                     if (connection.tag == "empty")
//                         return connection;
                    
//                     else if (connection.tag == n.tag)
//                         killSurrounding(connection);

//                     else if (connection.tag != n.tag)
//                         break;
//                 }
//             destroyConnections(n);                
//         }
//         return null;
//     }

//     public void destroyConnections(GameObject point)
//     {
        
//         Point p = point.GetComponent<Point>();

//         List<GameObject> group = new List<GameObject>();

//         foreach (GameObject connection in p.connections)
//         {
//             if (!group.Contains(connection))
//                 group.Add(connection);

//         }
//     }




//     public void destroyConnections(GameObject point)
//     {
        
//         Point p = point.GetComponent<Point>();

//         foreach (GameObject connection in p.connections)
//         {
//             Destroy(connection.transform.GetChild(0).gameObject);
//             connection.tag = empty;
//             Point c = connection.GetComponent<Point>();
//             c.connections.Clear();
//             Debug.Log("destroyed stone at ("+connection.transform.position.x+","+connection.transform.position.y+")");
//         }
//     }

// }

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