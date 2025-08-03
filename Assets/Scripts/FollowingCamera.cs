using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform _turretPivot; // Объект турели
    [SerializeField] private Vector3 _localOffset = new Vector3(0f, 15f, -45f); // Смещение камеры
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 5f;

    private void LateUpdate()
    {
        if (_turretPivot == null) return;
        Vector3 desiredPosition = _turretPivot.TransformPoint(_localOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _followSpeed);
        Vector3 forward = _turretPivot.forward;
        Quaternion desiredRotation = Quaternion.LookRotation(forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * _rotateSpeed);
    }
}
