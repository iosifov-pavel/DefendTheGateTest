using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField]
    private PrefabManager _prefabManager;
    [SerializeField]
    private LevelManager _levelManager;
    [SerializeField]
    private SceneLoadingManager _sceneManager;


    private SaveManager _saveManager;
    private EventManager _eventManager;

    public PrefabManager PrefabManager => _prefabManager;
    public LevelManager LevelManager => _levelManager;
    public SaveManager SaveManager => _saveManager;
    public EventManager EventManager => _eventManager;
    public SceneLoadingManager SceneManager => _sceneManager;

    public void Initialize()
    {
        _saveManager = new SaveManager();
        _saveManager.Initialize();
        _eventManager = new EventManager();
    }
}
