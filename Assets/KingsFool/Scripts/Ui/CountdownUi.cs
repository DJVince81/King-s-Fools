using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CountdownUi : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        float timeLeft = GameManager.Instance.TimeLeft;
        int minutes = (int)timeLeft / 60;
        int seconds = (int)timeLeft % 60;
        text.text = $"{minutes:00}:{seconds:00}";
    }
}
