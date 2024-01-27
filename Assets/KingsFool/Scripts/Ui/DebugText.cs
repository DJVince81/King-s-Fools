using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DebugText : MonoBehaviour
{
    private static readonly string[] trapControls = new string[]
    {
        "1/A",
        "2/B",
        "3/X",
        "4/Y",
    };

    public void DisplayObstaclesList(Obstacle[] obstacles)
    {
        string obstaclesList = "Obstacles list:\n";
        int maxControls = Mathf.Min(obstacles.Length, trapControls.Length);
        for (int i = 0; i < maxControls; i++)
        {
            obstaclesList += trapControls[i] + ": " + obstacles[i].gameObject.name + "\n";
        }

        GetComponent<TMP_Text>().text = obstaclesList;
    }
}
