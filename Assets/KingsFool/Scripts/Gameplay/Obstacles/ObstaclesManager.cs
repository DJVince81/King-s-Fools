using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager Instance;
    private List<Obstacle> obstacles;
    private List<Obstacle> visibleObstacles;

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

    public void ActivateObstacle(int index)
    {
        if (index < 0 || index >= obstacles.Count)
            return;
        visibleObstacles[index].Activate();
    }

    public void OnObstacleVisibilityChanged(object sender, System.EventArgs e)
    {
        Obstacle obstacle = (Obstacle)sender;
        if (obstacle.IsVisible)
            visibleObstacles.Add(obstacle);
        else
            visibleObstacles.Remove(obstacle);

        Debug.Log("Visible obstacles:");
        for (int i = 0; i < visibleObstacles.Count; i++)
            Debug.Log(i + ": " + visibleObstacles[i].gameObject.name);
    }
}
