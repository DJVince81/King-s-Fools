using System.Collections;
using UnityEngine;

public class TranslatingObject : Obstacle
{
    [SerializeField] private GameObject body, targetUpLine, targetPosition;
    private Renderer bodyRenderer;
    private float length;
    private bool isMoving;


    public float speed = 1f;
    public float AutomaticLiftingDelay = 1f;
    public bool invisibleDuringTheLiffing;


    private void Start()
    {
        bodyRenderer = body.GetComponent<Renderer>();
        length = bodyRenderer.bounds.size.y;
    }


    public IEnumerator Descent()
    {
        Vector3 targetPositionValue = targetPosition.transform.position + new Vector3(0, length / 2, 0);
        Vector3 direction = (targetPositionValue - body.transform.position).normalized;

        while (Vector3.Distance(body.transform.position, targetPositionValue) > 0.01f)
        {
            body.transform.position += speed * Time.deltaTime * direction;
            yield return null;
        }
        StartCoroutine(AutomaticLifting());
    }

    public IEnumerator AutomaticLifting()
    {
        yield return new WaitForSeconds(AutomaticLiftingDelay);
        if (invisibleDuringTheLiffing)
        {
            bodyRenderer.enabled = false;
        }
        Vector3 targetPosition = targetUpLine.transform.position - new Vector3(0, length / 2, 0);
        Vector3 direction = (targetPosition - body.transform.position).normalized;

        while (Vector3.Distance(body.transform.position, targetPosition) > 0.01f)
        {
            body.transform.position += speed * Time.deltaTime * direction;
            yield return null;
        }
        if (invisibleDuringTheLiffing)
        {
            bodyRenderer.enabled = true;
        }
        isMoving = false;
        isActivated = false;
    }

    public void Update()
    {
        if (isActivated && !isMoving)
        {
            isMoving = true;
            StartCoroutine(Descent());
        }
    }
}
