using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Transform _shootPoint;

    private float _secondsPerShoot;

    public void Setup(float secondsPerShoot)
    {
        _secondsPerShoot = secondsPerShoot;
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
        projectile.Setup(ChooseProjectile(), ApplicationController.Instance.Constants.BaseFlyTime, targetPosition);
    }

    private CannonObject ChooseProjectile()
    {
        var chance = Random.Range(0f, 1f);
        var targetType = chance <= 0.5f ? ObjectType.Ball : chance <= 0.75f ? ObjectType.Bomb : ObjectType.Coin;
        var projectile = ApplicationController.Instance.Managers.PrefabManager.GetCannonObject(targetType);
        return projectile;
    }
}
