using UnityEngine;

[CreateAssetMenu(fileName = "CannonObject", menuName = "Objects/cannonObject", order = 1)]
public class CannonObject : ScriptableObject
{
    [SerializeField]
    private Sprite _objectSprite;
    [SerializeField]
    private ObjectType _type;
    [SerializeField]
    private int _scoreChange;

    public Sprite Sprite => _objectSprite;
    public ObjectType Type => _type;
    public int ScoreChange => _scoreChange;

}

public enum ObjectType
{
    Ball,
    Bomb,
    Coin
}
