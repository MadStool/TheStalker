using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChaseTarget : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _stoppingDistance = 2f;
    [SerializeField] private float _activationDistance = 20f;
    [SerializeField] private Transform _target;

    private Rigidbody _rigidbody;

    public Transform Target { get => _target; set => _target = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        if (IsWithinActivationRange() == false)
        {
            StopMovement();
            return;
        }

        Vector3 direction = GetHorizontalDirectionToTarget();
        float distance = direction.magnitude;

        if (distance > _stoppingDistance)
        {
            MoveTowardsTarget(direction);
        }
        else
        {
            StopHorizontalMovement();
        }
    }

    private bool IsWithinActivationRange()
    {
        float sqrDistance = (_target.position - transform.position).sqrMagnitude;

        return sqrDistance <= _activationDistance * _activationDistance;
    }

    private Vector3 GetHorizontalDirectionToTarget()
    {
        Vector3 direction = _target.position - transform.position;
        direction.y = 0f;

        return direction;
    }

    private void MoveTowardsTarget(Vector3 direction)
    {
        Vector3 moveForce = direction.normalized * _moveSpeed;
        _rigidbody.linearVelocity = new Vector3(
            moveForce.x,
            _rigidbody.linearVelocity.y,
            moveForce.z
        );
    }

    private void StopMovement()
    {
        _rigidbody.linearVelocity = Vector3.zero;
    }

    private void StopHorizontalMovement()
    {
        _rigidbody.linearVelocity = new Vector3(
            0f,
            _rigidbody.linearVelocity.y,
            0f
        );
    }
}