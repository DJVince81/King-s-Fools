using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public static MainPanel Instance;
    private List<PlayerPanel> playerPanels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerPanels = new List<PlayerPanel>();
        }
        else
            Destroy(gameObject);
    }

    public void AddPlayerPanel(PlayerPanel playerPanel)
    {
        playerPanels.Add(playerPanel);
        playerPanel.Initialize(playerPanels.Count);
    }

    public void RemovePlayerPanel(PlayerPanel playerPanel)
    {
        if (playerPanel.isDisconnected) return;
        playerPanel.isDisconnected = true;
        StartCoroutine(WaitAndRemovePlayerPanel(playerPanel));
    }

    private IEnumerator<WaitForSeconds> WaitAndRemovePlayerPanel(PlayerPanel playerPanel)
    {
        yield return new WaitForSeconds(0.1f);
        playerPanels.Remove(playerPanel);
        Destroy(playerPanel.gameObject);
        for (int i = 0; i < playerPanels.Count; i++)
        {
            playerPanels[i].player.SetPlayerNumber(i + 1);
        }
    }
}