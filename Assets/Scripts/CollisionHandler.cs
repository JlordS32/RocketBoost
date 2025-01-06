using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay = 2f;
    [SerializeField] private AudioClip _crashSFX;
    [SerializeField] private AudioClip _successSFX;
    [SerializeField] private ParticleObject _particleObjects;

    // References
    private AudioSource _audioSource;

    // Variables
    private bool _isControllable;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
        _isControllable = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_isControllable) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSequence("Success", nameof(LoadNextLevel), _successSFX);
                break;
            default:
                StartSequence("Crash", nameof(ResetLevel), _crashSFX);
                break;
        }
    }

    private void StartSequence(string particleName, string functionName, AudioClip audio) {
        ParticleSystem particle = _particleObjects.GetParticleByName(particleName).Particle;
        particle.Play();
        _isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(audio);
        GetComponent<Movement>().enabled = false;
        Invoke(functionName, _levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}