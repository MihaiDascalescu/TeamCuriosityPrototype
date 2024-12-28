using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        MainMenu = 0,
        Playing = 10,
        End = 20
    }

    [SerializeField] private Transform startLocation; 
    [SerializeField] private Transform player;
    [SerializeField] private Transform lowerBoundary;
    [SerializeField] private List<Checkpoint> checkpoints;

    [SerializeField] private FinishFlag finishFlag;
    
    private GameState gameStates;

    public event EventHandler IsGameFinished;
    public event EventHandler OnGameRestarted; 
    
    public GameState GameStates
    {
        get => gameStates;
        set => gameStates = value;
    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameStates = GameState.MainMenu;
    }

    private void Start()
    {
        player.transform.position = startLocation.transform.position;
        finishFlag.IsFinished += EndGame;
    }

    public void RestartGame()
    {
        player.transform.position = startLocation.transform.position;
        gameStates = GameState.MainMenu;
        foreach (var varCheckpoint in checkpoints)
        {
            varCheckpoint.IsVisited = false;

        }
        OnGameRestarted?.Invoke(this, EventArgs.Empty);
    }
    private void EndGame(object sender, EventArgs e)
    {
        gameStates = GameState.End;
        IsGameFinished?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        int checkPointsVisited = 0;
        if (player.transform.position.y < lowerBoundary.position.y)
        {
            foreach (var varCheckpoint in checkpoints)
            {
                if (varCheckpoint.IsVisited)
                {
                    checkPointsVisited++;
                    player.transform.position = varCheckpoint.transform.position;
                }
            }

            if (checkPointsVisited == 0)
            {
                player.transform.position = startLocation.transform.position;
            }
           
        }
    }
}
