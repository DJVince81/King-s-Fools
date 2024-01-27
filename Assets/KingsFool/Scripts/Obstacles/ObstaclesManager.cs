using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    private List<Obstacle> obstacles;
    void Start()
    {
        obstacles = new List<Obstacle>();
        foreach (GameObject obstacle in transform)
        {
            if (obstacle.GetComponent<Obstacle>() != null)
                obstacles.Add(obstacle.GetComponent<Obstacle>());
        }
    }
}
