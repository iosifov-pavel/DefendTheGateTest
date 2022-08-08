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
        Debug.Log("PIU");
        var targetPosition = ApplicationController.Instance.CurrentLevel.CalculateShootTarget();
        transform.up = (targetPosition - (Vector2)transform.position).normalized;
        projectile.Setup(ChooseProjectile(), ApplicationController.Instance.Constants.BaseFlyTime, targetPosition);
    }

    private CannonObject ChooseProjectile()
    {
        var projectile = ApplicationController.Instance.Managers.PrefabManager.GetCannonObject(ObjectType.Ball);
        return projectile;
    }
}
