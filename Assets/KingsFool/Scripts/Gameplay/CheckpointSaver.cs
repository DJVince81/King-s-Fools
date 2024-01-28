using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckpointSaver : MonoBehaviour
{
    [SerializeField] private GameObject associatedCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() != null)
        {
            // Only save the checkpoint if it's further than the previous one
            if (GameManager.Instance.checkpointPosition.x < associatedCheckpoint.transform.localPosition.x)
            {
                GameManager.Instance.checkpointPosition = associatedCheckpoint.transform.localPosition;
                Debug.Log("Checkpoint saved");
            }
        }
    }
}
