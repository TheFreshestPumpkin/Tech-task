using UnityEngine;

public class EnemyHelicopter : MonoBehaviour
{
    [SerializeField]private float _moveSpeed = 10f;
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private Transform _targetPoint;

    [SerializeField] private GameObject _cabin;
    [SerializeField] private GameObject _topRotor;
    [SerializeField] private GameObject _tailRotor;

    private bool reachedTarget = false;
    private bool isDestroyed = false;

    public Transform TargetPoint { get => _targetPoint; set => _targetPoint = value; }

    private void Update()
    {
        if (isDestroyed) return;

        if (!reachedTarget && TargetPoint != null)
        {
            Vector3 direction = TargetPoint.position - transform.position;
            if (direction.magnitude > _stopDistance)
            {
                transform.position += direction.normalized * _moveSpeed * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                reachedTarget = true;
            }
        }
    }

    public void DestroyHelicopter()
    {
        if (isDestroyed) return;
        isDestroyed = true;

        ActivatePartPhysics(_cabin);
        ActivatePartPhysics(_topRotor);
        ActivatePartPhysics(_tailRotor);

        HelicopterSpawner.Instance.SpawnNext();
        Destroy(gameObject);
    }

    private void ActivatePartPhysics(GameObject part)
    {
        part.transform.parent = null;

        Rigidbody rb = part.GetComponent<Rigidbody>();
        Collider col = part.GetComponent<Collider>();

        if (rb != null) rb.useGravity = true;
        if (col != null) col.enabled = true;

        // Добавим эффект разрушения
        rb.AddExplosionForce(200f, transform.position, 5f);
        rb.AddTorque(Random.onUnitSphere * 100f);
    }
}
