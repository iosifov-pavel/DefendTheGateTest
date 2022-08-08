using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static LevelController _controller;
    [SerializeField]
    private PrefabManager _prefabManager;

    public PrefabManager PrefabManager => _prefabManager;
}
