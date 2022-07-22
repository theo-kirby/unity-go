using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Tromp-Taylor rules via Sensei's Library
// Go is played on a 19x19 square grid of points, by two players called Black and White.
    // the 19 x 19 square grid is constructed via a 2 dimensional array of gameobjects
    // each gameobject contains a Point class, which keeps track of the point's stone, adjacent stones(neighbors), & connected stones(group) 
// Each point on the grid may be colored black, white or empty.
    // the color of the stone occupying each point is kept track of in the point class
// A point P, not colored C, is said to reach C, if there is a path of (vertically or horizontally) adjacent points of P’s color from P to a point of color C.
    // the getGroup function implements a breadth first search algorithm to find all of the stones that are connected vertically or horizontally to any given stone
// Clearing a color is the process of emptying all points of that color that don’t reach empty.
    // the getReach and validGroup functions together calculate the 'reach' of a group of stones, which contains all of the liberties of the group
// Starting with an empty grid, the players alternate turns, starting with Black.
    // todo
// A turn is either a pass; or a move that doesn’t repeat an earlier grid coloring.[1]
    // todo
// A move consists of coloring an empty point one’s own color; then clearing the opponent color, and then clearing one’s own color.
    // the playStone function assigns a stone of the players color to one of the points in the array; the checkNeighbors finctionm
// The game ends after two consecutive passes.
    // todo
// A player’s score is the number of points of her color, plus the number of empty points that reach only her color.
    // todo
// The player with the higher score at the end of the game is the winner. Equal scores result in a tie.
    // todo


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

        connectStones(p);
        checkNeighbors(p);
    
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

    public bool validGroup(Point point)
    {
        bool libertyFound = false;
        foreach (Point reach in point.reached)
            if (reach.gameObject.tag == empty)
                libertyFound = true;
        if (libertyFound)
            return true;
        else
            return false;
    }

    public void killGroup(Point point)
    {
        foreach (Point member in point.group)
            Destroy(member.gameObject);
    }

    public void connectStones(Point point)
    {
        setConnections(point);
        getGroup(point);
        sendGroup(point);
        getNeighbors(point);
        getReach(point);
        sendReach(point);
    }

    public void checkNeighbors(Point point)
    {
        foreach (Point neighbor in point.neighbors)
            if (!validGroup(neighbor))
                killGroup(neighbor);
    }
}
