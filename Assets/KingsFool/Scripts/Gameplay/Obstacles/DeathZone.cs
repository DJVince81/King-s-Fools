using System.Collections;
using UnityEngine;

public class DeathZone : Obstacle
{
    public bool isProcessing = false;
    public float activationDuration = 1f;

    private IEnumerator AddCollision()
    {
        yield return new WaitForSeconds(activationDuration);
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void Update()
    {
        if (isActivated && !isProcessing)
        {
            isProcessing = true;
            GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(AddCollision());
        }
    }
}
