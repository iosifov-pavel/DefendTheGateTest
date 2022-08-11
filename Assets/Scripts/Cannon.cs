using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;

    private float _initialSecondsPerShoot;
    private float _secondsPerShoot;

    public void Setup(float initialSecondsPerShoot)
    {
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp += StopShooting;
        _initialSecondsPerShoot = initialSecondsPerShoot;
        _secondsPerShoot = GetShootTime();
        StartCoroutine(ShootRoutine());
    }

    private void StopShooting(object sender, bool e)
    {
        ApplicationController.Instance.Managers.EventManager.OnLevelTimerIsUp -= StopShooting;
        StopAllCoroutines();
    }

    private IEnumerator ShootRoutine()
    {
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= _secondsPerShoot)
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
        // тут можно было бы прикрутить шансы спавна разных объектов к данным уровня, но я решил оставить это в таком виде, так как в целом
        // нужные задачи такой разброс выполняет на базовом уровне
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
