using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    private PlayerData _data;

    public PlayerData PlayerData => _data;

    public void Initialize()
    {
        _data = new PlayerData();
        _data.AvaliableLevels.Add(0);
    }

    public void UpdatePlayerCoins(int coins)
    {
        _data.UpdateCoins(coins);
    }

    public void SetLevelAvaliable(int index, int coinsCost)
    {
        _data.SetLevelAvaliable(index, coinsCost);
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState?.Invoke(this, new KeyValuePair<ObjectType, int>(ObjectType.Coin, _data.Coins));
    }
}

[System.Serializable]
public struct PlayerData
{
    private int _coins;
    private List<int> _avaliableLevels;

    public int Coins => _coins;
    public List<int> AvaliableLevels
    {
        get
        {
            if(_avaliableLevels == null)
            {
                _avaliableLevels = new List<int>();
            }
            return _avaliableLevels;
        }
    }

    public void UpdateCoins(int coinsChange)
    {
        _coins += coinsChange;
    }

    private void SetCoins(int value)
    {
        _coins = value;
    }

    public void SetLevelAvaliable(int levelIndex, int coinsCost)
    {
        if(_avaliableLevels.Contains(levelIndex))
        {
            return;
        }
        _avaliableLevels.Add(levelIndex);
        UpdateCoins(-coinsCost);
    }

    public bool IsLevelAvaliable(int levelIndex)
    {
        return _avaliableLevels.Contains(levelIndex);
    }
}
