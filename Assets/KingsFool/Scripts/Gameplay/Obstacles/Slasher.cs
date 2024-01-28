using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : Obstacle
{

    public GameObject g;
    public Animator animator;
    public bool used;
    public int i;

    void Start()
    {
        used = false;
        i = 0;
    }

    void Update()
    {
        if (this.isActivated)
        {
            used = true;
            animator.SetBool("order", true);
        }
        if(used)
        {
            i++;
        }
        if (i == 10)
        {
            g.SetActive(true);
            used = false;
            i = 0;
            animator.SetBool("order", false);
        }
    }
}
