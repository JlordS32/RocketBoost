using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction _thrustButton;
    [SerializeField] private InputAction _rotation;
    [SerializeField] private float _thrustPower = 100f;
    [SerializeField] private float _rotationStrength = 100f;
    [SerializeField] private AudioClip _thrustSound;
    [SerializeField] private ParticleObject _particlesObjects;

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
        ParticleSystem mainEngineParticle = _particlesObjects.GetParticleByName("MainEngine").Particle;

        if (_thrustButton.IsPressed())
        {
            _rb.AddRelativeForce(Time.fixedDeltaTime * (_thrustPower * Vector3.up));

            if (!_audioSource.isPlaying) _audioSource.PlayOneShot(_thrustSound);
            if (!mainEngineParticle.isPlaying) mainEngineParticle.Play();
        }
        else
        {
            mainEngineParticle.Stop();
            _audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = _rotation.ReadValue<float>();
        ParticleSystem sideEngineThrust = _particlesObjects.GetParticleByName(rotationInput < 0 ? "RightEngine" : "LeftEngine").Particle;

        if (rotationInput < 0)
        {
            ApplyRotation(_rotationStrength);
            if (!sideEngineThrust.isPlaying) sideEngineThrust.Play();
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-_rotationStrength);
            if (!sideEngineThrust.isPlaying) sideEngineThrust.Play();
        }
        else
        {
            sideEngineThrust.Stop();
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