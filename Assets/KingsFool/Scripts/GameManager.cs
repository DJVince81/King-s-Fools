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

    private Dictionary<InputDevice, Player> players;

    private void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        if (Instance == null)
        {
            Instance = this;
            players = new Dictionary<InputDevice, Player>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
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

            // Se d�sabonner de l'�v�nement apr�s l'avoir g�r�
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ResetOneTime()
    {
        oneTime = true;
    }
}
