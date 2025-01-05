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
    private AudioSource _audioSource;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        } else {
            _audioSource.Stop();
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
        float rotationInput = _rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            ApplyRotation(_rotationStrength);
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-_rotationStrength);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        _rb.freezeRotation = false;
    }

    private void OnDisable()
    {
        _thrustButton.Disable();
        _rotation.Disable();
    }
}