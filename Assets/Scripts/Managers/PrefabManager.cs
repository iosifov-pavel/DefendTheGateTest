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
    private PlayerController _playerPrefab;
    [SerializeField]
    private CannonObject[] _presets;
    [SerializeField]
    private LevelPicker _levelPicker;

    public Cannon Cannon => _cannonPrefab;
    public BoxCollider2D Gate => _gatePrefab;
    public CannonProjectile Projectile => _projectilePrefab;
    public PlayerController Player => _playerPrefab;
    public LevelPicker LevelPicker => _levelPicker;

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
