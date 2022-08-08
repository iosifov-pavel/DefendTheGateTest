using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelData _levelData;

    private BoxCollider2D _gates;
    private Player _player;

    private void Start()
    {
        Setup(_levelData);
        ApplicationController.Instance.CurrentLevel = this;
    }

    public void Setup(LevelData data)
    {
        _levelData = data;
        LoadLevel();
    }

    private void LoadLevel()
    {
        CreateGates();
        CreatePlayer();
        CreateCannons();
    }

    private void CreateCannons()
    {
        foreach (var cannonPosition in _levelData.Cannons)
        {
            var newCannon = Instantiate(ApplicationController.Instance.Managers.PrefabManager.Cannon, cannonPosition, Quaternion.identity);
            newCannon.Setup(ApplicationController.Instance.Constants.CannonShootCooldown);
        }
    }

    private void CreatePlayer()
    {
        _player = Instantiate(ApplicationController.Instance.Managers.PrefabManager.Player, _gates.transform.position, Quaternion.identity);
    }

    private void CreateGates()
    {
        _gates = Instantiate(ApplicationController.Instance.Managers.PrefabManager.Gate.transform, _levelData.GatePosition, Quaternion.identity).GetComponent<BoxCollider2D>();
        _gates.transform.localScale = _levelData.GateSize;
    }

    public Vector2 CalculateShootTarget()
    {
        var posX = Random.Range(_gates.transform.position.x - _gates.bounds.extents.x, _gates.transform.position.x + _gates.bounds.extents.x);
        var posY = Random.Range(_gates.transform.position.y - _gates.bounds.extents.y, _gates.transform.position.y + _gates.bounds.extents.y);
        return new Vector2(posX,posY);
    }
}
