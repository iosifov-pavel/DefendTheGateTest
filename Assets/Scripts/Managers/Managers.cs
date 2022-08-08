using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField]
    private PrefabManager _prefabManager;
    [SerializeField]
    private LevelManager _levelManager;

    private SaveManager _saveManager;
    private EventManager _eventManager;

    public PrefabManager PrefabManager => _prefabManager;
    public LevelManager LevelManager => _levelManager;
    public SaveManager SaveManager => _saveManager;
    public EventManager EventManager => _eventManager;

    public void Initialize()
    {
        _saveManager = new SaveManager();
        _eventManager = new EventManager();
    }
}
