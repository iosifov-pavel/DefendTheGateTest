using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uRandom = UnityEngine.Random;
using uMath = UnityEngine.Mathf;

public class LevelController : MonoBehaviour
{
    private LevelData _levelData;
    private BoxCollider2D _gates;
    private PlayerController _player;
    private int _score;
    private int _coins;

    private void Awake()
    {
        ApplicationController.Instance.LevelController = this;
        Setup(ApplicationController.Instance.Managers.LevelManager.CurrentLevel);
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
        StartTimer();
    }

    private void StartTimer()
    {
        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        var timer = _levelData.LevelTime;
        var diff = 0f;
        while (timer > 0)
        {
            diff += Time.deltaTime;
            if (diff >= 1)
            {
                timer--;
                diff = 0;
                ApplicationController.Instance.Managers.EventManager.OnLevelTimerUpdate?.Invoke(this, timer);
            }
            yield return null;
        }
        EndLevel();
    }

    private void EndLevel()
    {
        var succsess = _score >= _levelData.WinScore;
        if(succsess)
        {
            ApplicationController.Instance.Managers.SaveManager.UpdatePlayerCoins(_coins);
        }
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp?.Invoke(this, succsess);
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
        var offset = ApplicationController.Instance.Constants.GatePositionOffset;
        var posX = uRandom.Range(_gates.transform.position.x - _gates.bounds.extents.x + offset, _gates.transform.position.x + _gates.bounds.extents.x - offset);
        var posY = uRandom.Range(_gates.transform.position.y - _gates.bounds.extents.y + offset, _gates.transform.position.y + _gates.bounds.extents.y - offset);
        return new Vector2(posX, posY);
    }

    private void ProjectilesHandler(object sender, KeyValuePair<CannonObject, bool> data)
    {
        switch (data.Key.Type)
        {
            case ObjectType.Ball:
                if (data.Value)
                {
                    UpdateScore(data.Key.ScoreChange);
                }
                else
                {
                    UpdateScore(-data.Key.ScoreChange);
                }
                break;
            case ObjectType.Bomb:
                if (data.Value)
                {
                    UpdateScore(-data.Key.ScoreChange);
                }
                break;
            case ObjectType.Coin:
                if (data.Value)
                {
                    UpdateCoins(data.Key.ScoreChange);
                }
                break;
        }
    }

    private void UpdateScore(int scoreChange)
    {
        _score = (int)uMath.Clamp(_score + scoreChange, 0, _levelData.MaxScore);
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState?.Invoke(this, new KeyValuePair<ObjectType, int>(ObjectType.Ball, _score));
    }

    private void UpdateCoins(int coinsChange)
    {
        _coins = (int)MathF.Max(0, _coins + coinsChange);
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState?.Invoke(this, new KeyValuePair<ObjectType, int>(ObjectType.Coin, _coins));
    }
}
