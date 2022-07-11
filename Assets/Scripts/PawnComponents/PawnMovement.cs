using FishNet.Object;
using UnityEngine;

public sealed class PawnMovement : NetworkBehaviour
{
    private PawnInput _input;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float gravityScale;

    private CharacterController _characterController;

    private Vector3 _velocity;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _input = GetComponent<PawnInput>();

        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 desiredVelocity = Vector3.ClampMagnitude(((transform.forward * _input.vertical) + (transform.right * _input.horizontal)) * speed, speed);

        _velocity.x = desiredVelocity.x;
        _velocity.z = desiredVelocity.z;

        if (_characterController.isGrounded)
        {
            _velocity.y = 0.0f;

            if (_input.jump)
            {
                _velocity.y = jumpSpeed;
            }
        }
        else
        {
            _velocity.y += Physics.gravity.y * gravityScale * Time.deltaTime;
        }

        _characterController.Move(_velocity * Time.deltaTime);
    }
}
