using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(MainPanel.Instance.transform);
        gameObject.transform.localScale = Vector3.one;
        Debug.Log("Move gameObject");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
