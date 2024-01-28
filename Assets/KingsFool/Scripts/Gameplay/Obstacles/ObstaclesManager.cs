using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager Instance;

    private List<Obstacle> obstacles;
    private List<Obstacle> visibleObstacles;

    [SerializeField] private List<TMP_Text> textList;

    private const int MAX_CONTROLS = 8;

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
        {
            if (!visibleObstacles.Contains(obstacle)) visibleObstacles.Add(obstacle);
        }
        else
        {
            if (visibleObstacles.Contains(obstacle)) visibleObstacles.Remove(obstacle);
        }
        SortVisibleObstaclesByXPosition();
        if (visibleObstacles.Count > MAX_CONTROLS)
        {
            visibleObstacles.RemoveAt(0);
        }

        int currentControlsAmount = Mathf.Min(visibleObstacles.Count, MAX_CONTROLS);
        for (int i = 0; i < MAX_CONTROLS; i++)
        {
            textList[i].text = (i < currentControlsAmount) ? visibleObstacles[i].gameObject.name : "";
        }
    }

    private void SortVisibleObstaclesByXPosition()
    {
        visibleObstacles.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));
    }
}
