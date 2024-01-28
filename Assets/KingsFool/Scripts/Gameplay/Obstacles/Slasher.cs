using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : Obstacle
{

    public GameObject boxCollider;
    public Animator animator;
    public int timer;

    void Update()
    {
        if (this.isActivated)
        {
            animator.SetBool("order", true);
            this.isActivated = false;
        }
        if(animator.GetBool("order"))
        {
            timer++;
        }
        if (timer >= 300)
        {
            if(animator.GetBool("order"))
            {
                boxcollider.SetActive(true);
                animator.SetBool("order", false);
            } else
            {
                timer = 0;
                boxcollider.SetActive(false);
            }
        }
    }
}
