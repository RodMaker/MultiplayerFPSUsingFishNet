using FishNet;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class LobbyView : View
{
    [SerializeField]
    private Button toggleReadyButton;

    [SerializeField]
    private TextMeshProUGUI toggleReadyButtonText;

    [SerializeField]
    private Button startGameButton;

    public override void Initialize()
    {
        toggleReadyButton.onClick.AddListener(() => Player.Instance.ServerSetIsReady(!Player.Instance.isReady));

        if (InstanceFinder.IsHost)
        {
            startGameButton.onClick.AddListener(() => GameManager.Instance.StartGame());

            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }

        base.Initialize();
    }

    private void Update()
    {
        if (!Initialized) return;

        toggleReadyButtonText.color = Player.Instance.isReady ? Color.green : Color.red;

        startGameButton.interactable = GameManager.Instance.canStart;
    }
}
