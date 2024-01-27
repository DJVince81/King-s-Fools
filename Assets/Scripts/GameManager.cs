using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject FoolPlayer;
    [SerializeField] private GameObject KingPlayer;

    private bool oneTime = true;

    private Dictionary<PlayerInput, Player> players;

    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        if (Instance == null)
        {
            Instance = this;
            players = new Dictionary<PlayerInput, Player>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void AddPlayer(PlayerInput playerInput, Player player)
    {
        players.Add(playerInput, player);
    }

    public void LaunchNewGame()
    {
        if (oneTime)
        {
            oneTime = false;
            ChooseRandomKing();
            if (players.Count >= 2)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene("Main");
            }
            Invoke(nameof(ResetOneTime), 0.25f);
        }
    }

    private void ChooseRandomKing()
    {
        int randomKingIndex = Random.Range(0, players.Count);
        players.ElementAt(randomKingIndex).Value.IsKing = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Vérifier si la scène chargée est celle que nous attendons
        if (scene.name == "Main")
        {
            GameObject cinemachineTargetGroup = new("TargetGroup");
            CinemachineTargetGroup cinemachine = cinemachineTargetGroup.AddComponent<CinemachineTargetGroup>();
            for (int i = 0; i < players.Count; i++)
            {
                bool isKing = players.ElementAt(i).Value.IsKing;
                GameObject newPlayer = Instantiate(isKing ? Instance.KingPlayer : Instance.FoolPlayer);
                newPlayer.name = (isKing ? "King" : "Fool") + (i + 1);
                if (!isKing)
                {
                    cinemachine.AddMember(newPlayer.transform, 1f, 3f);
                }
            }
            // SceneManager.MoveGameObjectToScene(cinemachineTargetGroup, scene);

            // Se désabonner de l'événement après l'avoir géré
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ResetOneTime()
    {
        oneTime = true;
    }
}
