using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 50f;
    [SerializeField] private float _gravityForce = 0f;
    [SerializeField] private float _fireRate = 0.2f;

    private float fireCooldown;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

#if UNITY_EDITOR
        bool isFiring = Input.GetMouseButton(0);
#else
        bool isFiring = Input.touchCount > 0;
#endif

        if (isFiring && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = _fireRate;
        }
    }

    public void Shoot()
    {
        Vector3 shootDirection = _firePoint.forward;

        GameObject bullet = Instantiate(
            _bulletPrefab,
            _firePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * _bulletSpeed;

        rb.AddForce(Vector3.down * _gravityForce, ForceMode.Acceleration);
    }

}
