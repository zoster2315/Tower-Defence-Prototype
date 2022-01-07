using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoords(pathFinder.StartCoords);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coords = new Vector2Int();
        if (resetPath)
            coords = pathFinder.StartCoords;
        else
            coords = gridManager.GetCoordinatesFromPosition(transform.position);

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coords);
        StartCoroutine(FollowPath());
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoords(path[i].coordinates);
            float travelPersent = 0;

            transform.LookAt(endPos);

            while (travelPersent < 1f)
            {
                travelPersent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPersent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
