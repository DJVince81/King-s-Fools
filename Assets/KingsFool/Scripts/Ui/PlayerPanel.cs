using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class PlayerPanel : MonoBehaviour
{
    public Player player;
    [HideInInspector] public bool isDisconnected = false;
    [SerializeField] Image playerImage;
    [SerializeField] RawImage panelBackground;
    [SerializeField] TMP_Text playerNumberText;
    [SerializeField] TMP_Text controllerText;
    [SerializeField] Color[] playerColors;
    [SerializeField] Sprite jesterSprite;
    [SerializeField] Sprite kingSprite;

    private PlayerInput playerInput;

    private void Start()
    {
        gameObject.transform.SetParent(MainPanel.Instance.transform);
        gameObject.transform.localScale = Vector3.one;
        playerInput = GetComponent<PlayerInput>();
        MainPanel.Instance.AddPlayerPanel(this);
    }

    public void Initialize(int playerNumber)
    {
        InputDevice inputDevice = playerInput.devices[0];
        player = new Player(playerNumber, inputDevice);
        player.PlayerChanged += OnPlayerChanged;
        GameManager.Instance.AddPlayer(inputDevice, player);
        player.PlayerNumber = playerNumber;
        playerNumberText.text = $"Player {playerNumber}";
        panelBackground.color = playerColors[playerNumber - 1];
        controllerText.text = inputDevice.name;
    }

    private void SetAsKing(bool isKing)
    {
        playerNumberText.color = isKing ? Color.yellow : Color.white;
        playerImage.sprite = isKing ? kingSprite : jesterSprite;
    }

    private void ChangePlayerNumber(int playerNumber)
    {
        playerNumberText.text = $"Player {playerNumber}";
    }

    public void Disconnect()
    {
        MainPanel.Instance.RemovePlayerPanel(this);
    }

    private void OnDestroy()
    {
        player.PlayerChanged -= OnPlayerChanged;
    }

    private void OnPlayerChanged(object sender, EventArgs e)
    {
        if (sender is Player player)
        {
            SetAsKing(player.IsKing);
            ChangePlayerNumber(player.PlayerNumber);
        }
    }
}
