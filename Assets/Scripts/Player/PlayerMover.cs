using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    // Camera
    private float _cameraAngle;
    private float _verticalMinAngle;
    private float _verticalMaxAngle;
    private float _horizontalTurnSensivity;
    private float _verticalTurnSensivity;
    // Movement
    [SerializeField] private Animator _animator;
    private float _gravityFactor;
    private float _jumpSpeed;
    private float _speed;
    private float _strafeSpeed;

    private CharacterController _controller;
    private Vector3 _verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _cameraAngle = _cameraTransform.localEulerAngles.x;                   // Camera
        _verticalMinAngle = -89;                                              // Camera
        _verticalMaxAngle = 89;                                               // Camera
        _horizontalTurnSensivity = 7;                                         // Camera + вращает персонажа
        _verticalTurnSensivity = 10;                                          // Camera
        _verticalVelocity = Vector3.down;
        _gravityFactor = 2;                // Работает только когда персонаж в прыжке, чтобы быстрее приземлятся.
        _jumpSpeed = 10;
        //_animator.SetBool("IsRunning", true);
    }

    private void Update()
    {
        // Camera
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;      // Camera
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;          // Camera
        _cameraAngle -= Input.GetAxis("Mouse Y") * _verticalTurnSensivity;                              // Camera
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);                 // Camera
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;                               // Camera

        transform.Rotate(Vector3.up * _horizontalTurnSensivity * Input.GetAxis("Mouse X"));             // Camera + вращает персонажа (если выделенно в отдельный компонент то вращать родителя)

        // Movement
        Vector3 movement = forward * Input.GetAxis("Vertical") * _speed + right * Input.GetAxis("Horizontal") * _strafeSpeed;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _verticalVelocity = Vector3.up * _jumpSpeed;
            else
                _verticalVelocity = Vector3.down;

            _controller.Move((movement + _verticalVelocity) * Time.deltaTime);
        }
        else                                            // Если объект в воздухе
        {
            Vector3 horizontalVelocity = _controller.velocity;
            horizontalVelocity.y = 0;
            _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;
            _controller.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
        }
    }
}
