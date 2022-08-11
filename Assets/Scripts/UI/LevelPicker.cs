using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPicker : MonoBehaviour
{
    [SerializeField]
    private Button _levelButton;
    [SerializeField]
    private GameObject _unavaliableTint;
    [SerializeField]
    private TMP_Text _levelName;
    [SerializeField]
    private TMP_Text _levelPriceText;

    private bool _avaliable;
    private LevelData _levelData;

    public void Setup(LevelData data, bool avaliable)
    {
        _levelData = data;
        SetAvaliableView(avaliable);
        _levelName.text = _levelData.LevelName;
        _levelPriceText.text = _levelData.LevelBuyCost.ToString();
        _levelButton.onClick.AddListener(PickerAction);
    }

    private void SetAvaliableView(bool avaliable)
    {
        _avaliable = avaliable;
        _unavaliableTint.SetActive(!_avaliable);
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
        ApplicationController.Instance.Managers.SaveManager.SetLevelAvaliable(_levelData.LevelIndex, _levelData.LevelBuyCost);
        SetAvaliableView(true);
    }

    private void PlayLevel()
    {
        ApplicationController.Instance.Managers.SceneManager.LoadLevelScene(_levelData);
    }
}
