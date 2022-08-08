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
