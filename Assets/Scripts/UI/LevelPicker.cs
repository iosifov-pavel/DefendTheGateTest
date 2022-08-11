using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPicker : MonoBehaviour
{
    [SerializeField]
    private Button _levelButton;

    private bool _avaliable;
    private LevelData _levelData;

    public void Setup(LevelData data, bool avaliable)
    {
        _avaliable = avaliable;
        _levelData = data;
        _levelButton.onClick.AddListener(PickerAction);
    }

    private void PickerAction()
    {
        if(_avaliable)
        {
            PlayLevel();
        }
        else
        {
            BuyLevel();
        }
    }

    private void BuyLevel()
    {
        var playerCoins = ApplicationController.Instance.Managers.SaveManager.PlayerData.Coins;
        if(playerCoins < _levelData.LevelBuyCost)
        {
            return;
        }
        ApplicationController.Instance.Managers.SaveManager.PlayerData.SetLevelAvaliable(_levelData.LevelIndex, _levelData.LevelBuyCost);
        _avaliable = true;
    }

    private void PlayLevel()
    {
        ApplicationController.Instance.Managers.SceneManager.LoadLevelScene(_levelData);
    }
}
