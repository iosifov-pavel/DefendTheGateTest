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

    public LevelController CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
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
        _managers = Instantiate<Managers>(_managers, transform);
        _constants = new Constants();
        ObjectPool.Setup(_poolHolder);
    }
}
