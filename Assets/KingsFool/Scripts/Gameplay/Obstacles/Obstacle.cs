using System;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public bool IsVisible
    {
        get => isVisible;
        set
        {
            if (isVisible != value)
            {
                isVisible = value;
                // Debug.Log("Obstacle " + gameObject.name + " is now " + (isVisible ? "visible" : "invisible"));
                ObstacleVisibilityChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private bool isVisible;
    public event EventHandler ObstacleVisibilityChanged;
    [SerializeField] AudioClip activationSound = null;

    protected bool isActivated;
    public void Activate()
    {
        Debug.Log("Activating obstacle " + gameObject.name);
        isActivated = true;
        if (activationSound != null) SfxPlayer.instance.Play(activationSound);
    }
}
