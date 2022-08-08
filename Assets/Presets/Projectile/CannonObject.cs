using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonObject", menuName = "Objects/cannonObject", order = 1)]
public class CannonObject : ScriptableObject
{
    [SerializeField]
    private Sprite _objectSprite;
    [SerializeField]
    private ObjectType _type;

    public Sprite Sprite => _objectSprite;
    public ObjectType Type => _type;

}

public enum ObjectType
{
    Ball,
    Bomb,
    Coin
}
