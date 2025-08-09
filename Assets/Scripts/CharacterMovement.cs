using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InputReader))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private CharacterController _controller;
    private Transform _cameraTransform;
    private InputReader _inputReader;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        _inputReader = GetComponent<InputReader>();
    }

    private void Update()
    {
        Vector2 input = _inputReader.ReadInput();

        if (_inputReader.HasInput())
            MoveCharacter(input);
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