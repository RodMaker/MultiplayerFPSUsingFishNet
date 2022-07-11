using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using FishNet.Connection;

public sealed class Player : NetworkBehaviour
{
    public static Player Instance { get; private set; }

    [SyncVar]
    public string username;

    [SyncVar]
    public bool isReady;

    [SyncVar]
    public Pawn controlledPawn;

    public override void OnStartServer()
    {
        base.OnStartServer();

        GameManager.Instance.players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        GameManager.Instance.players.Remove(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner) return;

        Instance = this;

        UIManager.Instance.Initialize();

        UIManager.Instance.Show<LobbyView>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ServerSetIsReady(!isReady);
        }

        /* used for testing purposes no longer needed
        if (Input.GetKeyDown(KeyCode.I))
        {
            ServerSpawnPawn();
        }
        */
    }

    public void StartGame()
    {
        GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion();

        GameObject pawnInstance = Instantiate(pawnPrefab);

        Spawn(pawnInstance, Owner);

        controlledPawn = pawnInstance.GetComponent<Pawn>();

        controlledPawn.controllingPlayer = this;

        TargetPawnSpawned(Owner);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSpawnPawn()
    {
        StartGame();
    }

    public void StopGame()
    {
        if (controlledPawn != null && controlledPawn.IsSpawned) controlledPawn.Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSetIsReady(bool value)
    {
        isReady = value;
    }

    [TargetRpc]
    private void TargetPawnSpawned(NetworkConnection networkConnection)
    {
        UIManager.Instance.Show<MainView>();
    }

    [TargetRpc]
    public void TargetPawnKilled(NetworkConnection networkConnection)
    {
        UIManager.Instance.Show<RespawnView>();
    }

    /* used for testing purposes, no longer needed
    [ServerRpc]
    private void ServerSpawnPawn()
    {
        GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion();

        GameObject pawnInstance = Instantiate(pawnPrefab);

        Spawn(pawnInstance, Owner);
    }
    */
}
