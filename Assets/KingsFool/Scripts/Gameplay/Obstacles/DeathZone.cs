using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : Obstacle
{
    public override void Activate()
    {
        isActivated = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
