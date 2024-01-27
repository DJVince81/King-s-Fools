using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager Instance;

    [SerializeField] DebugText debugText;
    private List<Obstacle> obstacles;
    private List<Obstacle> visibleObstacles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            obstacles = new List<Obstacle>();
            visibleObstacles = new List<Obstacle>();
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            GameObject go = child.gameObject;
            if (go.GetComponent<Obstacle>() != null)
            {
                Obstacle obstacle = go.GetComponent<Obstacle>();
                obstacle.ObstacleVisibilityChanged += OnObstacleVisibilityChanged;
                obstacles.Add(obstacle);
            }
        }
    }

    public void ActivateObstacle(int index)
    {
        if (index < 0 || index >= visibleObstacles.Count) return;
        visibleObstacles[index].Activate();
    }

    public void OnObstacleVisibilityChanged(object sender, System.EventArgs e)
    {
        Obstacle obstacle = (Obstacle)sender;
        if (obstacle.IsVisible)
            visibleObstacles.Add(obstacle);
        else
            visibleObstacles.Remove(obstacle);

        debugText.DisplayObstaclesList(visibleObstacles.ToArray());
    }
}
