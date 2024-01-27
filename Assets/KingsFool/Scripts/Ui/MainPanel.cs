using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    public static MainPanel Instance;
    private int nbPlayer = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPlayer(GameObject playerObject)
    {
        nbPlayer = transform.childCount;
        playerObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Player " + nbPlayer;
    }

    public void RemovePlayer()
    {
        StartCoroutine(_RemovePlayer());
    }
    private IEnumerator _RemovePlayer()
    {
        // Attendre pendant 2 secondes
        yield return new WaitForSeconds(0.1f);
        nbPlayer = transform.childCount;
        for (int i = 0; i < nbPlayer; i++)
        {
            transform.GetChild(i).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Player " + (i + 1);
        }
    }
}