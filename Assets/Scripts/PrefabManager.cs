using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabManager : MonoBehaviour
{
    [SerializeField]
    private Cannon _cannonPrefab;
    [SerializeField]
    private BoxCollider2D _gatePrefab;
    [SerializeField]
    private CannonProjectile _projectilePrefab;
    [SerializeField]
    private Player _playerPrefab;
    [SerializeField]
    private CannonObject[] _presets;

    public Cannon Cannon => _cannonPrefab;
    public BoxCollider2D Gate => _gatePrefab;
    public CannonProjectile Projectile => _projectilePrefab;
    public Player Player => _playerPrefab;

    public CannonObject GetCannonObject(ObjectType type)
    {
        CannonObject cannonObject = null;
        try
        {
            cannonObject = _presets.First(p => p.Type == type);
        }
        catch
        {
            Debug.LogError("there is no such preset");
        }
        return cannonObject;
    }
}
