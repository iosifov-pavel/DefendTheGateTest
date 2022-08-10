using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private Button _playButton;

    private void Start()
    {
        _playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        ApplicationController.Instance.Managers.SceneManager.LoadMainMenu();
    }
}
