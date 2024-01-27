using UnityEngine;

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
