using UnityEngine;

public class King : MonoBehaviour
{
    public void OnButtonPressed(int index)
    {
        ObstaclesManager.Instance.ActivateObstacle(index);
    }
}
