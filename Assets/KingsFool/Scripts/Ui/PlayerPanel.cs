using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    private int playerNumber;

    [SerializeField] TMP_Text playerNumberText;
    [SerializeField] TMP_Text controllerText;


    public void Initialize()
    {
        Debug.Log("PlayerPanel.Initialize()");
        playerNumber = transform.GetSiblingIndex() + 1;
        playerNumberText.text = "Player " + playerNumber;
    }
}
