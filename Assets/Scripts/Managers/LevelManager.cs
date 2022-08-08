using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<LevelData> _levels;

    private LevelData _currentLevel;
    public List<LevelData> Levels => _levels;
    public LevelData CurrentLevel => _currentLevel;

    public void SetCurrentLevel(LevelData data)
    {
        _currentLevel = data;
    }
}
