using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceble;

    [SerializeField] Tower tower;
    public bool IsPlaceble { get { return isPlaceble; } }

    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coords = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coords = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceble)
                gridManager.BlockNode(coords);
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coords).isWalkable && !pathFinder.WillBlockPath(coords))
        {
            if (tower.CreateTower(tower, transform.position))
            {
                gridManager.BlockNode(coords);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
