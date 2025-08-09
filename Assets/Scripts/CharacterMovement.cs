using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private CharacterController _controller;
    private Transform _cameraTransform;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector2 input = GetInput();

        if (ShouldMove(input))
            MoveCharacter(input);
    }

    private Vector2 GetInput()
    {
        return new Vector2(
            Input.GetAxis(HorizontalAxis),
            Input.GetAxis(VerticalAxis)
        );
    }

    private bool ShouldMove(Vector2 input)
    {
        return input.magnitude > 0.01f;
    }

    private void MoveCharacter(Vector2 input)
    {
        Vector3 moveDirection = CalculateMoveDirection(input);
        RotateCharacter(moveDirection);
        MoveForward();
    }

    private Vector3 CalculateMoveDirection(Vector2 input)
    {
        Vector3 direction = _cameraTransform.right * input.x +
                           _cameraTransform.forward * input.y;
        direction.y = 0f;

        return direction.normalized;
    }

    private void RotateCharacter(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );
    }

    private void MoveForward()
    {
        _controller.Move(transform.forward * _moveSpeed * Time.deltaTime);
    }
}