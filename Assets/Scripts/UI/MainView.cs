using UnityEngine;
using TMPro;

public sealed class MainView : View
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    private void Update()
    {
        if (!Initialized) return;

        Player player = Player.Instance;

        if (player == null || player.controlledPawn == null) return;

        healthText.text = $"Health {Player.Instance.controlledPawn.health}";
    }
}
