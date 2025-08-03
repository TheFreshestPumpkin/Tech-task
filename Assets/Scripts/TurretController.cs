using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _turretPivot;   //по Y
    [SerializeField] private Transform _barrelPivot;   //по X
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _maxElevation = 60f;
    [SerializeField] private float _minElevation = -5f;
    [SerializeField] private Camera _cam;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            AimAtScreenPoint(touchPosition);
        }
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            AimAtScreenPoint(mousePosition);
        }
#endif
    }

    private void AimAtScreenPoint(Vector2 screenPos)
    {
        Ray ray = _cam.ScreenPointToRay(screenPos);
        Plane ground = new(Vector3.up, _turretPivot.position);

        if (ground.Raycast(ray, out float hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Vector3 flatDirection = targetPoint - _turretPivot.position;
            flatDirection.y = 0f;

            if (flatDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(flatDirection);
                _turretPivot.rotation = Quaternion.Slerp(_turretPivot.rotation, targetRot, Time.deltaTime * _rotationSpeed);
            }
            Vector3 directionToTarget = targetPoint - _barrelPivot.position;
            Vector3 localDir = _barrelPivot.InverseTransformDirection(directionToTarget.normalized);
            float elevationAngle = Mathf.Atan2(localDir.y, localDir.z) * Mathf.Rad2Deg;
            elevationAngle = Mathf.Clamp(elevationAngle, _minElevation, _maxElevation);
            Vector3 currentLocalAngles = _barrelPivot.localEulerAngles;
            currentLocalAngles.x = elevationAngle;
            if (currentLocalAngles.x < 0f) currentLocalAngles.x += 360f;

            _barrelPivot.localEulerAngles = currentLocalAngles;

        }
    }
}
