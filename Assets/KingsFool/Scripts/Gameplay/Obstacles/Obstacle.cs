using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected bool isActivated;
    public abstract void Activate();
}
