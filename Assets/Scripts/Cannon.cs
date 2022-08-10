using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;

    private float _initialSecondsPerShoot;
    private float _secondsPerShoot;

    public void Setup(float initialSecondsPerShoot)
    {
        _initialSecondsPerShoot = initialSecondsPerShoot;
        _secondsPerShoot = GetShootTime();
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        var timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;
            if(timer >= _secondsPerShoot)
            {
                Shoot();
                _secondsPerShoot = GetShootTime();
                timer = 0;
            }
            yield return null;
        }
    }

    private void Shoot()
    {
        var projectile = ObjectPool.GetObject(ApplicationController.Instance.Managers.PrefabManager.Projectile, ObjectType.Ball, _shootPoint.position, Quaternion.identity);
        projectile.Active = true;
        var targetPosition = ApplicationController.Instance.LevelController.CalculateShootTarget();
        transform.up = (targetPosition - (Vector2)transform.position).normalized;
        var baseFlyTime = ApplicationController.Instance.Constants.BaseFlyTime;
        var randomModifier = ApplicationController.Instance.Constants.FlyRandomModifier;
        var flyTime = Random.Range(baseFlyTime - randomModifier, baseFlyTime);
        projectile.Setup(ChooseProjectile(), flyTime, targetPosition);
    }

    private CannonObject ChooseProjectile()
    {
        var chance = Random.Range(0f, 1f);
        var targetType = chance <= 0.5f ? ObjectType.Ball : chance <= 0.75f ? ObjectType.Bomb : ObjectType.Coin;
        var projectile = ApplicationController.Instance.Managers.PrefabManager.GetCannonObject(targetType);
        return projectile;
    }

    private float GetShootTime()
    {
        return Random.Range(_initialSecondsPerShoot - ApplicationController.Instance.Constants.FlyRandomModifier, _initialSecondsPerShoot + ApplicationController.Instance.Constants.FlyRandomModifier);
    }
}
