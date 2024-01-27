using System;
using System.Collections;
using UnityEngine;

public class RotatingObject : Obstacle
{
    public GameObject body;
    public bool directionToLeft = false;

    public float delayReset = 1f;
    public float angleTarget = 90, rotationSpeed = 1.0f;
    private bool isMoving;


    public IEnumerator Rotate()
    {
        float directionMultiplier = directionToLeft ? -1 : 1;
        float angle = 0.0f;
        while (angle < angleTarget)
        {
            angle += rotationSpeed;
            body.transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * directionMultiplier);
            yield return null;
        }
        yield return new WaitForSeconds(delayReset);

        // Rotate back to the starting position
        angle = 0.0f;
        while (angle < angleTarget)
        {
            angle += rotationSpeed;
            body.transform.Rotate(new Vector3(0, 0, 1), -rotationSpeed * directionMultiplier); // Note the negative sign
            yield return null;
        }

        isMoving = false;
        isActivated = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }
        if (isActivated && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Rotate());
        }
    }

    public override void Activate()
    {
        isActivated = true;
    }
}
