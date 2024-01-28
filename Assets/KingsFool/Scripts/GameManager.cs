using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 checkpointPosition;
    public float TimeLeft
    {
        get => timeLeft;
        set
        {
            if (timeLeft != value)
            {
                timeLeft = value;
            }
        }
    }
    private float timeLeft;

    [SerializeField] private GameObject FoolPlayer;
    [SerializeField] private GameObject KingPlayer;
    [SerializeField] private float countdownDuration = 10f;

    private bool oneTime = true;
    private bool isGameRunning = false;

    private Dictionary<InputDevice, Player> players;

    private void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        if (Instance == null)
        {
            Instance = this;
            players = new Dictionary<InputDevice, Player>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ResetPlayers()
    {
        players.Clear();
    }

    public void AddPlayer(InputDevice InputDevice, Player player)
    {
        players.Add(InputDevice, player);
    }

    public void RemovePlayer(InputDevice InputDevice)
    {
        players.Remove(InputDevice);
    }

    public void LaunchNewGame()
    {
        if (oneTime)
        {
            oneTime = false;
            if (players.Count >= 2)
            {
                ChooseRandomKing();
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene("Main");
            }
            Invoke(nameof(ResetOneTime), 0.25f);
        }
    }

    public void LaunchNewGame(GameObject winner)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players.ElementAt(i).Value.IsKing = false;
        }
        players[winner.GetComponent<PlayerInput>().devices[0]].IsKing = true;
        if (players.Count >= 2)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene("Main");
        }
    }

    private void ChooseRandomKing()
    {
        int randomKingIndex = UnityEngine.Random.Range(0, players.Count);
        players.ElementAt(randomKingIndex).Value.IsKing = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // V�rifier si la sc�ne charg�e est celle que nous attendons
        if (scene.name == "Main")
        {
            GameObject cinemachineTargetGroup = new("TargetGroup");
            CinemachineTargetGroup cinemachine = cinemachineTargetGroup.AddComponent<CinemachineTargetGroup>();
            for (int i = 0; i < players.Count; i++)
            {
                Player player = players.ElementAt(i).Value;
                bool isKing = player.IsKing;
                GameObject newPlayer = Instantiate(isKing ? Instance.KingPlayer : Instance.FoolPlayer);
                newPlayer.name = (isKing ? "King" : "Fool") + (i + 1);
                // Sets the device of the player
                newPlayer.GetComponent<PlayerInput>().SwitchCurrentControlScheme(player.InputDevice);
                if (!isKing)
                {
                    cinemachine.AddMember(newPlayer.transform, 1f, 3f);
                }
            }

            StartGame();

            // Se d�sabonner de l'�v�nement apr�s l'avoir g�r�
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void FixedUpdate()
    {
        if (isGameRunning)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft <= 0f)
            {
                isGameRunning = false;
                EndGame();
            }
        }
    }

    private void StartGame()
    {
        TimeLeft = countdownDuration;
        isGameRunning = true;
    }

    private void EndGame()
    {
        ResetPlayers();
        SceneManager.LoadScene("CharacterSelection");
    }

    private void ResetOneTime()
    {
        oneTime = true;
    }
}
