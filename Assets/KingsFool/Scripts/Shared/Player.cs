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

    public InputDevice InputDevice
    {
        get => inputDevice;
        set
        {
            if (inputDevice != value)
            {
                inputDevice = value;
                PlayerChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private InputDevice inputDevice;

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

    public Player(int playerNumber, InputDevice inputDevice)
    {
        PlayerNumber = playerNumber;
        InputDevice = inputDevice;
        IsKing = false;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        PlayerNumber = playerNumber;
    }
}
