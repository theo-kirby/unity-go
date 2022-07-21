using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int x;
    public int y;
    public Stone stone;
    public List<Point> connections = new List<Point>();
    public List<Point> group = new List<Point>();
    public List<Point> dupGroup = new List<Point>();
    public List<Point> reached = new List<Point>();
    public List<Point> neighbors = new List<Point>();




}
