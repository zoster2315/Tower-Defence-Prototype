using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
                path.Add(child.GetComponent<Waypoint>());
        }
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint wp in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = wp.transform.position;
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
