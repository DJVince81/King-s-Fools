using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool oneTime = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchNewGame()
    {
        if (oneTime)
        {
            oneTime = false;
            for (int i = 0; i < MainPanel.Instance.nbPlayer; i++)
            {
                Debug.Log(MainPanel.Instance.transform.GetChild(i).GetComponent<PlayerInput>());
            }
            Invoke("ResetOneTime", 0.25f);
        }
    }

    private void ResetOneTime()
    {
        oneTime = true;
    }
}
