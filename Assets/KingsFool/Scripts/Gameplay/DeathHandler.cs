using System.Collections;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerDeath"))
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (player.transform.position.y < -15f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        playerSprite.SetActive(false);
        player.GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        player.transform.localPosition = GameManager.Instance.checkpointPosition;
        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        playerSprite.SetActive(true);
    }

}
