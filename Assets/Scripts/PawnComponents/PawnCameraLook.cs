using FishNet.Object;
using UnityEngine;

public sealed class PawnCameraLook : NetworkBehaviour
{
    private PawnInput _input;

    [SerializeField]
    private Transform myCamera;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float xMax;

    private Vector3 _eulerAngles;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _input = GetComponent<PawnInput>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        myCamera.GetComponent<Camera>().enabled = IsOwner;

        myCamera.GetComponent<AudioListener>().enabled = IsOwner;
    }

    private void Update()
    {
        if (!IsOwner) return;

        _eulerAngles.x -= _input.mouseY;

        _eulerAngles.x = Mathf.Clamp(_eulerAngles.x, xMin, xMax);

        myCamera.localEulerAngles = _eulerAngles;

        transform.Rotate(0.0f, _input.mouseX, 0.0f, Space.World);
    }
}
