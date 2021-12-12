using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    void Start()
    {
        StartCoroutine(FollowPath());
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
    }
}
