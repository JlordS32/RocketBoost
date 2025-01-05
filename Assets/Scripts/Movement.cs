using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction _thrustButton;
    [SerializeField] float _thrustPower;

    // References
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _thrustButton.Enable();
    }

    private void FixedUpdate() {
        // if (_thrustButton.IsPressed()) {
        //     _rb.AddRelativeForce(Vector3.up * _thrustPower * Time.fixedDeltaTime);
        // }
        
        // Aternative way
        if (_thrustButton.IsPressed()) {
            Vector3 newVelocity = _rb.linearVelocity + Vector3.up * _thrustPower * Time.fixedDeltaTime;

            _rb.linearVelocity = newVelocity;
        }

        Debug.Log(_rb.linearVelocity.y);
    }

    private void OnDisable()
    {
        _thrustButton.Disable();
    }
}