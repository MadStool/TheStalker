using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GravityHandler : MonoBehaviour
{
    private const float GroundedCheckInterval = 0.1f;
    private const float GroundedVerticalVelocity = -2f;

    [Header("Gravity Settings")]
    [SerializeField] private float _gravityMultiplier = 1f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _nextGroundedCheckTime;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        CheckGroundedStatus();
        ApplyGravityForce();
        MoveWithGravity();
    }

    private void CheckGroundedStatus()
    {
        if (Time.time > _nextGroundedCheckTime)
        {
            _nextGroundedCheckTime = Time.time + GroundedCheckInterval;

            if (_controller.isGrounded && _velocity.y < 0)
            {
                _velocity.y = GroundedVerticalVelocity;
            }
        }
    }

    private void ApplyGravityForce()
    {
        _velocity.y += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
    }

    private void MoveWithGravity()
    {
        _controller.Move(_velocity * Time.deltaTime);
    }
}