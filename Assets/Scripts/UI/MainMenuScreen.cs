using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField]
    private ScrollRect _levelScroll;

    private void Awake()
    {
        FillWithLevels();
    }

    private void FillWithLevels()
    {
        var levels = ApplicationController.Instance.Managers.LevelManager.Levels;
        foreach( var level in levels )
        {
            var levelIndex = levels.IndexOf(level)+1;
            var isLevelAvaliable = ApplicationController.Instance.Managers.SaveManager.PlayerData.IsLevelAvaliable(levelIndex);
            var levelPicker = Instantiate(ApplicationController.Instance.Managers.PrefabManager.LevelPicker, _levelScroll.content);
            levelPicker.Setup(level, isLevelAvaliable);
        }
    }
}
