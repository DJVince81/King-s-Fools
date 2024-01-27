using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject FoolPlayer;
    [SerializeField] private GameObject KingPlayer;

    private bool oneTime = true;

    private static Dictionary<int, PlayerInput> players;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        players = new Dictionary<int, PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchNewGame()
    {
        if (oneTime)
        {
            oneTime = false;
            players.Clear();
            for (int i = 0; i < MainPanel.Instance.nbPlayer; i++)
            {
                players.Add(i, MainPanel.Instance.transform.GetChild(i).GetComponent<PlayerInput>());
            }
            if (MainPanel.Instance.nbPlayer >= 3)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene("Main");
            }
            Invoke("ResetOneTime", 0.25f);
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Vérifier si la scène chargée est celle que nous attendons
        if (scene.name == "Main")
        {
            // Création des nouveaux PlayerInput dans la nouvelle scène
            int kingNbPlayer = UnityEngine.Random.Range(0, players.Count);
            GameObject cinemachineTargetGroup = new GameObject("TargetGroup");
            CinemachineTargetGroup cinemachine = cinemachineTargetGroup.AddComponent<CinemachineTargetGroup>();
            for (int i = 0; i < players.Count; i++)
            {
                bool isKing = kingNbPlayer == i;
                GameObject nouveauJoueur = Instantiate(isKing?Instance.KingPlayer:Instance.FoolPlayer);
                nouveauJoueur.name = (isKing?"King":"Fool") + (i + 1);
                if(!isKing)
                {
                    cinemachine.AddMember(nouveauJoueur.transform, 1f, 3f);
                }
                SceneManager.MoveGameObjectToScene(nouveauJoueur, scene);
            }
            SceneManager.MoveGameObjectToScene(cinemachineTargetGroup, scene);

            // Se désabonner de l'événement après l'avoir géré
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void ResetOneTime()
    {
        oneTime = true;
    }
}
