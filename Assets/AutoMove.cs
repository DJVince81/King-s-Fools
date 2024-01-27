using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoMove : MonoBehaviour
{
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(MainPanel.Instance.transform);
        gameObject.transform.localScale = Vector3.one;
        MainPanel.Instance.AddPlayer(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void Disconnect()
    {
        Destroy(gameObject);
        MainPanel.Instance.RemovePlayer();
    }
}
