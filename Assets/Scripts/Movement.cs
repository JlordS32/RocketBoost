using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction _thrustButton;
    [SerializeField] InputAction _rotation;
    [SerializeField] float _thrustPower = 100f;
    [SerializeField] float _rotationStrength = 100f;

    // References
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _thrustButton.Enable();
        _rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (_thrustButton.IsPressed())
        {
            _rb.AddRelativeForce(Vector3.up * _thrustPower * Time.fixedDeltaTime);
        }

        // Alternative way
        // if (_thrustButton.IsPressed())
        // {
        //     float horizontalInput = Input.GetAxis("Horizontal");
        //     Vector3 newVelocity = _rb.linearVelocity + new Vector3(horizontalInput * Time.fixedDeltaTime, _thrustPower * Time.fixedDeltaTime, 0);

        //     _rb.linearVelocity = newVelocity;
        // }
    }

    private void ProcessRotation()
    {
        float rotationInput =_rotation.ReadValue<float>();

        if (rotationInput < 0) {
            ApplyRotation(_rotationStrength);
        }
        else if (rotationInput > 0) {
            ApplyRotation(-_rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame) {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        _thrustButton.Disable();
        _rotation.Disable();
    }
}