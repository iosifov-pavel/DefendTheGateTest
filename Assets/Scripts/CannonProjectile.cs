using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour, IPoolable
{
    [SerializeField]
    private SpriteRenderer _sprite;
    [SerializeField]
    private Sprite _targetSprite;

    private CannonObject _data;
    private bool _deflected;

    public bool Active { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    public void Setup(CannonObject data, float flyTime, Vector2 flyPosition)
    {
        _data = data;
        StartCoroutine(FlyToTarget(flyTime, flyPosition));
    }

    private IEnumerator FlyToTarget(float time, Vector2 targetPosition)
    {
        var timer = 0f;
        var startPosition = transform.position;
        while(timer <= time && !_deflected)
        {
            timer += Time.deltaTime;
            var newPosition = Vector2.Lerp(startPosition, targetPosition, timer / time);
            transform.position = newPosition;
            yield return null;
        }
    }
}
