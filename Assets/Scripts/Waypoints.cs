using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public List<Transform> GetPath()
    {
        List<Transform> waypoints = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i));
        }

        return waypoints;
    }
}
