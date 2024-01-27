using System;
using UnityEngine.InputSystem;

public class Player
{
    public event EventHandler PlayerChanged;

    public int PlayerNumber
    {
        get => playerNumber;
        set
        {
            if (playerNumber != value)
            {
                playerNumber = value;
                PlayerChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private int playerNumber;

    public PlayerInput PlayerInput
    {
        get => playerInput;
        set
        {
            if (playerInput != value)
            {
                playerInput = value;
                PlayerChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private PlayerInput playerInput;

    public bool IsKing
    {
        get => isKing;
        set
        {
            if (isKing != value)
            {
                isKing = value;
                PlayerChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private bool isKing;

    public Player(int playerNumber, PlayerInput playerInput)
    {
        PlayerNumber = playerNumber;
        PlayerInput = playerInput;
        IsKing = false;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        PlayerNumber = playerNumber;
    }
}
