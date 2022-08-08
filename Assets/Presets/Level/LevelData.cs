using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Level", menuName ="DefendTheGate/Level", order = 2)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int _levelIndex;
    [SerializeField]
    private string _levelName;
    [SerializeField]
    private float _levelTime;
    [SerializeField]
    private int _winScore;
    [SerializeField]
    private Vector3 _gatePosition;
    [SerializeField]
    private Vector2 _gateSize;
    [SerializeField]
    private Vector3[] _cannonPositions;

    public Vector3 GatePosition => _gatePosition;
    public Vector2 GateSize => _gateSize;
    public Vector3[] Cannons => _cannonPositions;
    public string LevelName => _levelName;
    public float LevelTime => _levelTime;
    public int WinScore => _winScore;
    public int LevelIndex => _levelIndex;
}
