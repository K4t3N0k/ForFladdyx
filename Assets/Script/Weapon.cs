using Mirror;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private CameraController _cameraController;

    private void Update()
    {
        if (!isLocalPlayer) return;

       if (Input.GetMouseButtonDown(0))
       {
            Shoot();
       }
    }

    private Vector3 GetBulletSpawnDirection()
    {
        return _cameraController.Direction;
    }

    [Command]
    private void SpawnBullet(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        Bullet instance = Instantiate(_bulletPrefab, spawnPosition, Quaternion.LookRotation(spawnDirection));
        NetworkServer.Spawn(instance.gameObject);
    }

    private void Shoot()
    {
        Vector3 spawnDirection = GetBulletSpawnDirection();
        Vector3 spawnPosition = transform.position + spawnDirection * 0.7f;
        SpawnBullet(spawnPosition, spawnDirection);
    }
}
