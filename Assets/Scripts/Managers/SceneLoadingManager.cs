using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class SceneLoadingManager : MonoBehaviour
{
    [SerializeField]
    private SceneData[] _scenes;
    public void LoadMainMenu()
    {
        var mainMenuData = _scenes.First(s => s.SceneType == SceneType.MainMenu);
        SceneManager.LoadScene(mainMenuData.SceneIndex);
    }

    public void LoadLevelScene(LevelData levelData)
    {
        var levelSceneData = _scenes.First(s => s.SceneType == SceneType.Level);
        ApplicationController.Instance.Managers.LevelManager.SetCurrentLevel(levelData);
        SceneManager.LoadScene(levelSceneData.SceneIndex);
    }
}

[System.Serializable]
public struct SceneData
{
    [SerializeField]
    public SceneType SceneType;
    [SerializeField]
    public int SceneIndex;
}

public enum SceneType
{
    MainMenu,
    Level
}
