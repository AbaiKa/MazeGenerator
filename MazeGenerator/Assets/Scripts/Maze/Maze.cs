using UnityEngine;

public class Maze
{
    public MazeCell[,] cells;
    public Vector2Int finishPosition;
}

public class MazeCell
{
    public int x;
    public int y;

    public int distanceFromStart;

    public bool leftWall = true;
    public bool bottomWall = true;
    public bool floor = true;

    public bool visited = false;

    public bool finish = false;
}
