using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _speed = 5f;

    private CameraController _cameraController;
    private CharacterController _characterController;

    private void Awake()
    {
        _cameraController = GetComponentInChildren<CameraController>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        Vector3 inputDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            inputDirection.z += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputDirection.z -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection.x += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputDirection.x -= 1f;
        }
        Vector3 moveDirection = _cameraController.TransformDirection(inputDirection) * _speed;
        moveDirection += Vector3.down * 10f;
        _characterController.Move(moveDirection * Time.deltaTime);
    }
}
