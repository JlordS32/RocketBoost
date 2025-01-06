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
            StartThrusting(mainEngineParticle);
        }
        else
        {
            StopThrusting(mainEngineParticle);
        }
    }

    private void StopThrusting(ParticleSystem mainEngineParticle)
    {
        mainEngineParticle.Stop();
        _audioSource.Stop();
    }

    private void StartThrusting(ParticleSystem mainEngineParticle)
    {
        _rb.AddRelativeForce(Time.fixedDeltaTime * (_thrustPower * Vector3.up));

        if (!_audioSource.isPlaying) _audioSource.PlayOneShot(_thrustSound);
        if (!mainEngineParticle.isPlaying) mainEngineParticle.Play();
    }

    private void ProcessRotation()
    {
        float rotationInput = _rotation.ReadValue<float>();
        ParticleSystem leftEngineParticle = _particlesObjects.GetParticleByName("LeftEngine").Particle;
        ParticleSystem rightEngineParticle = _particlesObjects.GetParticleByName("RightEngine").Particle;

        if (rotationInput != 0) {
            Rotate(leftEngineParticle, rightEngineParticle, rotationInput);
        }
        else
        {
            StopRotate(leftEngineParticle, rightEngineParticle);
        }
    }

    private void Rotate(ParticleSystem leftEngineParticle, ParticleSystem rightEngineParticle, float rotationThisFrame) {
        ApplyRotation(-_rotationStrength * rotationThisFrame);
        Debug.Log(rotationThisFrame);

        if (rotationThisFrame < 0.001f)
        {
            rightEngineParticle.Play();
            leftEngineParticle.Stop();
        }

        if (rotationThisFrame > 0.001f)
        {
            leftEngineParticle.Play();
            rightEngineParticle.Stop();
        }
    }

    private static void StopRotate(ParticleSystem leftEngineParticle, ParticleSystem rightEngineParticle)
    {
        leftEngineParticle.Stop();
        rightEngineParticle.Stop();
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