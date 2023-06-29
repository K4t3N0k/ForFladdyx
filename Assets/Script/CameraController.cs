using UnityEngine;
using Mirror;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private float _sensitivity = 1f;
    private Camera _camera;
    private Vector3 _angles;

    public Vector3 Direction
    {
        get => transform.forward;
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        float inputX = Input.GetAxis("Mouse Y");
        float inputY = Input.GetAxis("Mouse X");
        _angles = new Vector3(Mathf.Clamp(_angles.x - inputX, -88.0f, 88.0f), _angles.y + inputY, 0.0f);
        _camera.transform.rotation = Quaternion.Euler(_angles);
    }

    public Vector3 TransformDirection(Vector3 direction)
    {
        Vector3 cameraDirection = _camera.transform.TransformDirection(direction);
        cameraDirection.y = 0.0f;
        return cameraDirection.normalized;
    }
}
