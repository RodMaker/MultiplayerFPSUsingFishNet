using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PawnWeapon : NetworkBehaviour
{
    private Pawn _pawn;

    private PawnInput _input;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float shotDelay;

    private float _timeUntilNextShot;

    [SerializeField]
    private Transform firePoint;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        _pawn = GetComponent<Pawn>();

        _input = GetComponent<PawnInput>();
    }

    private void Update()
    {
        if (!IsOwner) return;

        if (_timeUntilNextShot <= 0.0f)
        {
            if (_input.fire)
            {
                ServerFire(firePoint.position, firePoint.forward);

                _timeUntilNextShot = shotDelay;
            }
        }
        else
        {
            _timeUntilNextShot -= Time.deltaTime;
        }
    }

    [ServerRpc]
    private void ServerFire(Vector3 firePointPosition, Vector3 firePointDirection)
    {
        if (Physics.Raycast(firePointPosition, firePointDirection, out RaycastHit hit) && hit.transform.TryGetComponent(out Pawn pawn))
        {
            pawn.ReceiveDamage(damage);
        }
    }
}
