using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField]
    private Cannon _cannonPrefab;
    [SerializeField]
    private Transform _gatePrefab;
    [SerializeField]
    private CannonProjectile _projectilePrefab;
    [SerializeField]
    private Player _playerPrefab;

    public Cannon Cannon => _cannonPrefab;
    public Transform Gate => _gatePrefab;
    public CannonProjectile Projectile => _projectilePrefab;
    public Player Player => _playerPrefab;
}
