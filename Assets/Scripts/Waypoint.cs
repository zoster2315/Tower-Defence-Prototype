using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceble;
    [SerializeField] GameObject tower;

    private void OnMouseDown()
    {
        if (isPlaceble)
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            isPlaceble = false;
        }
    }
}
