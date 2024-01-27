using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] TMP_Text playerNumberText;
    [SerializeField] TMP_Text controllerText;

    public void Initialize(int playerNumber, string controllerName)
    {
        playerNumberText.text = $"Player {playerNumber}";
        controllerText.text = controllerName;
    }
}
