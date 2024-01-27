using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager Instance;
    private List<Obstacle> obstacles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            obstacles = new List<Obstacle>();
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            GameObject obstacle = child.gameObject;
            if (obstacle.GetComponent<Obstacle>() != null)
                obstacles.Add(obstacle.GetComponent<Obstacle>());
        }
    }

    public void ActivateObstacles()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.Activate();
        }
    }
}
