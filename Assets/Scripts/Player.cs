using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private int _score;
    private int _coins;

    public void Setup()
    {
        ApplicationController.Instance.Events.OnCannonProjectileEvent += ProjectilesHandler;
    }

    private void ProjectilesHandler(object sender, KeyValuePair<CannonObject,bool> data)
    {
        switch(data.Key.Type)
        {
            case ObjectType.Ball:
                if(data.Value)
                {
                    UpdateScore(1);
                }
                else
                {
                    UpdateScore(-1);
                }
                break;
            case ObjectType.Bomb:
                if(data.Value)
                {
                    UpdateScore(-2);
                }
                break;
            case ObjectType.Coin:
                if(data.Value)
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
