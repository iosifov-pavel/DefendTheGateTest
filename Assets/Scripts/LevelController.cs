using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private LevelData _levelData;

    private void Start()
    {
        Setup(_levelData);
    }

    public void Setup(LevelData data)
    {
        _levelData = data;
        LoadLevel();
    }

    private void LoadLevel()
    {
        var gates = Instantiate(ApplicationController.Instance.Managers.PrefabManager.Gate, _levelData.GatePosition, Quaternion.identity);
        gates.transform.localScale = _levelData.GateSize;
        var player = Instantiate(ApplicationController.Instance.Managers.PrefabManager.Player, gates.transform.position, Quaternion.identity);
        foreach(var cannonPosition in _levelData.Cannons)
        {
            var newCannon = Instantiate(ApplicationController.Instance.Managers.PrefabManager.Cannon, cannonPosition, Quaternion.identity);
            newCannon.transform.up = (gates.transform.position - newCannon.transform.position).normalized;
            newCannon.Setup(ApplicationController.Instance.Constants.CannonShootCooldown);
        }
    }
}
