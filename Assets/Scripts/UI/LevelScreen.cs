using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _timer;
    [SerializeField]
    private TMP_Text _coinsCount;
    [SerializeField]
    private RectTransform _progressBar;
    [SerializeField]
    private Transform _progressBarFiller;
    [SerializeField]
    private RectTransform _winScoreLine;
    [SerializeField]
    private TMP_Text _winScoreText;
    [SerializeField]
    private GameObject _endLevelWindow;
    [SerializeField]
    private TMP_Text _endLevelText;
    [SerializeField]
    private Button _okButton;

    private LevelData _data;

    private void Awake()
    {
        Setup(ApplicationController.Instance.Managers.LevelManager.CurrentLevel);
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerUpdate += UpdateTimer;
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState += UpdateUI;
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp += EndLevel;
    }

    public void Setup(LevelData data)
    {
        _endLevelWindow.SetActive(false);
        _okButton.onClick.AddListener(GoToMainMenu);
        _data = data;
        UpdateTimer(this, _data.LevelTime);
        SetScoreRequirements();
        SetCoins(0);
        UpdateProgressBar(0);
    }

    private void SetCoins(int value)
    {
        _coinsCount.text = value.ToString();
    }

    private void SetScoreRequirements()
    {
        _winScoreText.text = _data.WinScore.ToString();
        var progressBarSize = _progressBar.rect.height;
        var scale = _data.WinScore / (float)_data.MaxScore;
        _winScoreLine.anchoredPosition = new Vector2(_winScoreLine.anchoredPosition.x, progressBarSize * scale);
    }

    private void UpdateTimer(object sender, float timerValue)
    {
        _timer.text = GetFormattedTime(timerValue);
    }

    private void UpdateProgressBar(float score)
    {
        var newScale = Mathf.Clamp(score / _data.MaxScore, 0, 1);
        _progressBarFiller.localScale = new Vector3(_progressBar.localScale.x, newScale, _progressBar.localScale.z);
    }

    private void EndLevel(object sender, bool succsess)
    {
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerUpdate -= UpdateTimer;
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState -= UpdateUI;
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp -= EndLevel;
        _endLevelText.text = succsess ? ApplicationController.Instance.Constants.winTextKey : ApplicationController.Instance.Constants.loseTextKey;
        _endLevelWindow.SetActive(true);
    }

    private void UpdateUI(object sender, KeyValuePair<ObjectType, int> state)
    {
        if (state.Key == ObjectType.Ball)
        {
            UpdateProgressBar(state.Value);
        }
        else
        {
            _coinsCount.text = state.Value.ToString();
        }
    }

    private void GoToMainMenu()
    {
        ApplicationController.Instance.Managers.SceneManager.LoadMainMenu();
    }


    private string GetFormattedTime(float seconds)
    {
        var minutes = (int)seconds / 60;
        var hours = minutes / 60;
        string resultString = null;
        if (hours == 0)
        {
            resultString = String.Format("{0:0#}:{1:0#}", minutes - 60 * hours, seconds - minutes * 60);
        }
        else
        {
            resultString = String.Format("{0:0#}:{1:0#}:{2:0#}", hours, minutes - 60 * hours, seconds - minutes * 60);
        }
        return resultString;
    }
}
