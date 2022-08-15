using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAssistant : MonoBehaviour
{
    public static MazeAssistant main;

    [SerializeField] private Transform _cellsContainer;
    [SerializeField] private Cell _cell;

    public MazeGenerator m_Generator;

    private List<GameObject> _maze_objects = new List<GameObject>();
    private void Awake() => main = this;
    public void CreateMaze(int width, int height)
    {
        Maze maze = m_Generator.GenerateMaze(width, height);

        ClearMazeArea();

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell cell = Instantiate(_cell, new Vector3(x * _cell.floor.transform.localScale.x, 0, y * _cell.floor.transform.localScale.z), Quaternion.identity);
                cell.gameObject.transform.SetParent(_cellsContainer);
                cell.leftWall.SetActive(maze.cells[x, y].leftWall);
                cell.bottomWall.SetActive(maze.cells[x, y].bottomWall);
                cell.floor.SetActive(maze.cells[x, y].floor);

                if (maze.cells[x, y].finish)
                {
                    cell.floor.GetComponent<MeshRenderer>().material.color = Color.blue;
                    cell.floor.AddComponent<FinishFloor>();
                }
                _maze_objects.Add(cell.gameObject);
            }
        }

        _maze_objects.Add(GameAssistant.Instance.SpawnPlayerOnFirstCell(maze.cells[0, 0].x, maze.cells[0, 0].y));
    }

    private void ClearMazeArea()
    {
        foreach (GameObject m_o in _maze_objects)
        {
            Destroy(m_o);
        }

        _maze_objects.Clear();
    }
}
