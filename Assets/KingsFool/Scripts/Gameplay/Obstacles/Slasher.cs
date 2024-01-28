using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : Obstacle
{

    public GameObject boxCollider;
    public Animator animator;
    public int timer;

    void FixedUpdate()
    {
        if (this.isActivated)
        {
            animator.SetBool("order", true);
            this.isActivated = false;
            timer = 0;
        }
        if(animator.GetBool("order"))
        {
            timer++;
        }
        if (timer >= 300)
        {
            if(animator.GetBool("order"))
            {
                boxCollider.SetActive(true);
                animator.SetBool("order", false);
            } else
            {
                boxCollider.SetActive(false);
            }
        }
    }
}
