using UnityEngine;

public class King : MonoBehaviour
{
    public void OnButtonsPressed()
    {
        ObstaclesManager.Instance.ActivateObstacles();
    }
}
