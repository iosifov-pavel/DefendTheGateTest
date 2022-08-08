using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uRandom = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelData _levelData;

    private BoxCollider2D _gates;
    private PlayerController _player;
    private int _score;
    private int _coins;

    private void Start()
    {
        Setup(_levelData);
        ApplicationController.Instance.LevelController = this;
    }

    public void Setup(LevelData data)
    {
        _levelData = data;
        ApplicationController.Instance.Managers.EventManager.OnCannonProjectileEvent += ProjectilesHandler;
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
        var posX = uRandom.Range(_gates.transform.position.x - _gates.bounds.extents.x, _gates.transform.position.x + _gates.bounds.extents.x);
        var posY = uRandom.Range(_gates.transform.position.y - _gates.bounds.extents.y, _gates.transform.position.y + _gates.bounds.extents.y);
        return new Vector2(posX,posY);
    }

    private void ProjectilesHandler(object sender, KeyValuePair<CannonObject, bool> data)
    {
        switch (data.Key.Type)
        {
            case ObjectType.Ball:
                if (data.Value)
                {
                    UpdateScore(1);
                }
                else
                {
                    UpdateScore(-1);
                }
                break;
            case ObjectType.Bomb:
                if (data.Value)
                {
                    UpdateScore(-2);
                }
                break;
            case ObjectType.Coin:
                if (data.Value)
                {
                    UpdateCoins(1);
                }
                break;
        }
    }

    private void UpdateScore(int scoreChange)
    {
        _score = (int)MathF.Max(0, _score + scoreChange);
        Debug.Log(_score);
    }

    private void UpdateCoins(int coinsChange)
    {
        _coins = (int)MathF.Max(0, _coins + coinsChange);
    }
}
