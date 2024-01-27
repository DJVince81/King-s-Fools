using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
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

    public void DisplayObstaclesList(Obstacle[] obstacles)
    {
        int maxControls = Mathf.Min(obstacles.Length, trapControls.Length);
        for (int i = 0; i < maxControls; i++)
        {
            textList[i].text = (i >= obstacles.Length)?"-----":obstacles[i].gameObject.name;
        }

    }
}
