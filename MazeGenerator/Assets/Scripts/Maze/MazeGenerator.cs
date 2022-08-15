using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public Maze GenerateMaze(int width, int height)
    {
        MazeCell[,] cells = new MazeCell[width, height];

        // Filling the maze with cells
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeCell { x = x, y = y };
            }
        }

        #region small introduction
        // Disable unnecessary objects
        // Since the cell is not a square, you will need to remove protruding objects

        /* Left wall
         * | 
         * |  /
         * | /Floor
         * |/_________
         *    Bottom wall
         */
        #endregion
        // Disable walls on the left
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, height - 1].leftWall = false;
            cells[x, height - 1].floor = false;
        }
        // Disable walls on the right
        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[width - 1, y].bottomWall = false;
            cells[width - 1, y].floor = false;
        }

        RemoveWalls(cells, width, height);

        Maze maze = new Maze();

        maze.cells = cells;
        maze.finishPosition = MazeFinish(cells, width, height);

        return maze;
    }

    // "recursive backtracker" algorithm
    private void RemoveWalls(MazeCell[,] maze, int width, int height)
    {
        MazeCell current = maze[0, 0];
        current.visited = true;
        current.distanceFromStart = 0;

        Stack<MazeCell> stack = new Stack<MazeCell>();
        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int x = current.x;
            int y = current.y;

            // Checking the cell
            // If we are here for the first time adding to the list of unvisitedNeighbours
            if (x > 0 && !maze[x - 1, y].visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].visited) unvisitedNeighbours.Add(maze[x, y - 1]);

            if (x < width - 2 && !maze[x + 1, y].visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < height - 2 && !maze[x, y + 1].visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            // If we have at least one unvisited object
            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell chosen = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.visited = true;

                stack.Push(chosen);

                chosen.distanceFromStart = current.distanceFromStart + 1;
                current = chosen;
            }
            else
            {
                // If there are no unvisited neighbors, this is a dead end.
                // Turn back
                current = stack.Pop();
            }
          
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.x == b.x)
        {
            if (a.y > b.y) a.bottomWall = false;
            else b.bottomWall = false;
        }
        else
        {
            if (a.x > b.x) a.leftWall = false;
            else b.leftWall = false;
        }
    }

    private Vector2Int MazeFinish(MazeCell[,] maze, int width, int height)
    {
        MazeCell furthest = maze[0, 0];

        // check the cell farthest from the finish line by width
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, height - 2];
            if (maze[x, 0].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, 0];
        }

        // check the cell farthest from the finish line by height
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 2, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[width - 2, y];
            if (maze[0, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[0, y];
        }

        // the farthest point on the left side of the maze
        if (furthest.x == 0) furthest.leftWall = false;
        // the farthest point on the bottom side of the maze.
        else if (furthest.y == 0) furthest.bottomWall = false;
        // the farthest point on the right side of the maze.
        else if (furthest.x == width - 2) maze[furthest.x + 1, furthest.y].leftWall = false;
        // the farthest point on the up side of the maze.
        else if (furthest.y == height - 2) maze[furthest.x, furthest.y + 1].bottomWall = false;

        furthest.finish = true;

        return new Vector2Int(furthest.x, furthest.y);
    }
}
