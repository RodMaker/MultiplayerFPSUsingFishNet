using FishNet.Object;
using UnityEngine;

public sealed class PawnInput : NetworkBehaviour
{
    private Pawn _pawn;

    public float horizontal;
    public float vertical;

    public float mouseX;
    public float mouseY;

    public float sensitivity;

    public bool jump;

    public bool fire;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _pawn = GetComponent<Pawn>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        jump = Input.GetButton("Jump");

        fire = Input.GetButton("Fire1");
    }
}
