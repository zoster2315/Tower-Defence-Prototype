using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceble;

    [SerializeField] Tower tower;
    public bool IsPlaceble { get { return isPlaceble; } }

    private void OnMouseDown()
    {
        if (isPlaceble)
        {
            if (tower.CreateTower(tower, transform.position))
                isPlaceble = false;
        }
    }
}
