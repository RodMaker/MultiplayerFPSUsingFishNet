using UnityEngine;
using UnityEngine.UI;

public sealed class RespawnView : View
{
    [SerializeField]
    private Button respawnButton;

    public override void Initialize()
    {
        respawnButton.onClick.AddListener(() => Player.Instance.ServerSpawnPawn());

        base.Initialize();
    }
}
