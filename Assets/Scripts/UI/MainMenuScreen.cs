using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _levelScroll;
    [SerializeField]
    private TMP_Text _playerCoins;


    private void Awake()
    {
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState += SetCoins;
        FillWithLevels();
        SetCoins(ApplicationController.Instance.Managers.SaveManager.PlayerData.Coins);
    }

    private void OnDestroy()
    {
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState -= SetCoins;
    }

    private void SetCoins(object sender, KeyValuePair<ObjectType, int> state)
    {
        SetCoins(state.Value);
    }

    private void SetCoins(int coins)
    {
        _playerCoins.text = coins.ToString();
    }

    private void FillWithLevels()
    {
        var levels = ApplicationController.Instance.Managers.LevelManager.Levels;
        foreach( var level in levels )
        {
            var levelIndex = levels.IndexOf(level);
            var isLevelAvaliable = ApplicationController.Instance.Managers.SaveManager.PlayerData.IsLevelAvaliable(levelIndex);
            isLevelAvaliable |= level.LevelBuyCost == 0;  
            var levelPicker = Instantiate(ApplicationController.Instance.Managers.PrefabManager.LevelPicker, _levelScroll.content);
            levelPicker.Setup(level, isLevelAvaliable);
        }
    }
}
