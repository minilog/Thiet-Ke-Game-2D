using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public GameObject Projectile;
    public Transform ShootPoint;
    public Transform CrouchShootPoint;

    private void Awake()
    {
        ObjectsInGame.PlayerDamage = this;
    }
}
