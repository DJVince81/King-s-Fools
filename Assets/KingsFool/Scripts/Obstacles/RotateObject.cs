using System.Collections;
using UnityEngine;



public class RotateObject : Obstacles
{
    public GameObject body;
    public float angleTarget = 90, rotationSpeed = 1.0f;
    private bool isMoving;


    public IEnumerator Rotate()
    {
        float angle = 0.0f;
        while (angle < angleTarget)
        {
            angle += rotationSpeed;
            body.transform.Rotate(new Vector3(0, 0, 1), rotationSpeed);
            yield return null;
        }
        isMoving = false;
        isActivated = false;
    }

    private void Update()
    {
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
