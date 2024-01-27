using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

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
            Instance = this;
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
        // V�rifier si la sc�ne charg�e est celle que nous attendons
        if (scene.name == "Main")
        {
            // Cr�ation des nouveaux PlayerInput dans la nouvelle sc�ne
            bool kingSpawned = false;
            for (int i = 0; i < players.Count; i++)
            {
                bool isKing = (UnityEngine.Random.Range(0, 10) > 7) && !kingSpawned;
                GameObject nouveauJoueur = Instantiate(isKing?Instance.KingPlayer:Instance.FoolPlayer);
                nouveauJoueur.name = (isKing?"King":"Fool") + (i + 1);
                SceneManager.MoveGameObjectToScene(nouveauJoueur, scene);
            }

            // Se d�sabonner de l'�v�nement apr�s l'avoir g�r�
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void LoadPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log(players[i].gameObject.name);
        }
    }

    private void ResetOneTime()
    {
        oneTime = true;
    }
}
