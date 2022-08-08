using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    public static ApplicationController Instance;
    [SerializeField]
    private Managers _managers;
    [SerializeField]
    private Transform _poolHolder;

    private Constants _constants;
    private LevelController _currentLevel;
    private EventManager _eventManager;

    public LevelController CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }
    public Managers Managers => _managers;
    public Constants Constants => _constants;
    public EventManager Events => _eventManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }

    private void InitManagers()
    {
        _managers = Instantiate(_managers, transform);
        _constants = new Constants();
        _eventManager = new EventManager();
        ObjectPool.Setup(_poolHolder);
    }
}
