using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    public Vector2Int StartCoords { get { return startCoords; } }

    [SerializeField] Vector2Int endCoords;
    public Vector2Int EndCoords { get { return endCoords; } }

    Node startNode;
    Node endNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            endNode = grid[endCoords];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }


    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoords);
    }

    public List<Node> GetNewPath(Vector2Int coords)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coords);
        return BuildPath();
    }

    void ExploreNeighbours()
    {
        if (gridManager == null)
            return;
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int coordinates = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(coordinates))
            {
                neighbors.Add(grid[coordinates]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coords)
    {
        startNode.isWalkable = true;
        endNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;

        frontier.Enqueue(grid[coords]);
        reached.Add(coords, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
            if (currentSearchNode.coordinates == endCoords)
            {
                isRunning = false;
            }
        }

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coords)
    {
        if (coords == startCoords || coords == endCoords)
            return true;
        if (grid.ContainsKey(coords) && grid[coords].isPath)
        {
            bool previousState = grid[coords].isWalkable;

            grid[coords].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coords].isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}
