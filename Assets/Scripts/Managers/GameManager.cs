using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using UnityEngine; // can be deleted if we ever remove the debug log

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SyncObject]
    public readonly SyncList<Player> players = new();

    [SyncVar]
    public bool canStart;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Debug.Log(players.Count); used for testing purposes, no longer needed

        if (!IsServer) return;

        canStart = players.All(player => player.isReady);

        //Debug.Log($"Can start = {canStart}"); used for testing purposes, no longer needed
    }

    [Server]
    public void StartGame()
    {
        if (!canStart) return;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].StartGame();
        }
    }

    public void StopGame()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].StopGame();
        }
    }
}
