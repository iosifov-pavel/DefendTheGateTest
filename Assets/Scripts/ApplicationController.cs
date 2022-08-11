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
    private LevelController _currentLevelController;

    public LevelController LevelController
    {
        get => _currentLevelController;
        set => _currentLevelController = value;
    }
    public Managers Managers => _managers;
    public Constants Constants => _constants;

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
        _constants = new Constants();
        _managers = Instantiate(_managers, transform);
        _managers.Initialize();
        ObjectPool.Setup(_poolHolder);
    }
}
