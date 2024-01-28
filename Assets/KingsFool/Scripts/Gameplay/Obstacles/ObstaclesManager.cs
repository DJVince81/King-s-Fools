using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    public static ObstaclesManager Instance;

    private List<Obstacle> obstacles;
    private List<Obstacle> visibleObstacles;

    [SerializeField] private List<TMP_Text> textList;

    private static readonly string[] trapControls = new string[]
    {
        "1/A",
        "2/B",
        "3/X",
        "4/Y",
        "5/↓",
        "6/↑",
        "7/→",
        "8/←",
    };

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
            visibleObstacles.Remove(obstacle);

        int maxControls = Mathf.Min(obstacles.Count, trapControls.Length);
        for (int i = 0; i < maxControls; i++)
        {
            textList[i].text = (i >= obstacles.Count) ? "-----" : obstacles[i].gameObject.name;
        }
    }
}
