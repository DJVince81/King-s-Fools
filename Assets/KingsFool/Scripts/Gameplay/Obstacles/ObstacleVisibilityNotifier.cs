using UnityEngine;

public class ObstacleVisibilityNotifier : MonoBehaviour
{
    [SerializeField] private Obstacle obstacle;

    private void OnBecameVisible()
    {
        obstacle.IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        obstacle.IsVisible = false;
    }
}
