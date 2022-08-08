using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Level", menuName ="DefendTheGate/Level", order = 2)]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private Vector3 _gatePosition;
    [SerializeField]
    private Vector2 _gateSize;
    [SerializeField]
    private Vector3[] _cannonPositions;

    public Vector3 GatePosition => _gatePosition;
    public Vector2 GateSize => _gateSize;
    public Vector3[] Cannons => _cannonPositions;
}
