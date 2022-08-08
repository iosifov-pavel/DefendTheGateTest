using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour, IPoolable
{
    [SerializeField]
    private SpriteRenderer _sprite;
    [SerializeField]
    private Transform _body;
    [SerializeField]
    private Transform _target;

    private CannonObject _data;
    private bool _deflected;

    public bool Active { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    public void Setup(CannonObject data, float flyTime, Vector2 flyPosition)
    {
        _target.position = flyPosition;
        _body.position = transform.position;
        _data = data;
        _sprite.sprite = _data.Sprite;
        StartCoroutine(FlyToTarget(flyTime, flyPosition));
    }

    private IEnumerator FlyToTarget(float time, Vector2 targetPosition)
    {
        var timer = 0f;
        var startPosition = _body.position;
        while(timer <= time && !_deflected)
        {
            timer += Time.deltaTime;
            var newPosition = Vector2.Lerp(startPosition, targetPosition, timer / time);
            _body.position = newPosition;
            yield return null;
        }
        Active = false;
    }
}
