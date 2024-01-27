using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPanel : MonoBehaviour
{
    public static MainPanel Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
