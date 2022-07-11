using FishNet;
using UnityEngine;
using UnityEngine.UI;

public sealed class MultiplayerMenu : MonoBehaviour
{
    [SerializeField]
    private Button hostButton;

    [SerializeField]
    private Button connectButton;

    private void Start()
    {
        hostButton.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });

        connectButton.onClick.AddListener(() => InstanceFinder.ClientManager.StartConnection());
    }
}
