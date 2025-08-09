using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _target;

    public Transform Target { get => _target; set => _target = value; }

    private void Update()
    {
        if (_target == null)
            return;

        Vector3 direction = GetHorizontalDirectionToTarget();

        if (ShouldRotate(direction))
            RotateTowards(direction);
    }

    private Vector3 GetHorizontalDirectionToTarget()
    {
        Vector3 direction = _target.position - transform.position;
        direction.y = 0f;

        return direction;
    }

    private bool ShouldRotate(Vector3 direction)
    {
        return direction.sqrMagnitude > 0.01f;
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }
}