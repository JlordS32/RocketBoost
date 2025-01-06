using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay = 2f;
    [SerializeField] private AudioClip _crashSFX;
    [SerializeField] private AudioClip _successSFX;
    [SerializeField] private Particles[] _particles;

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
        foreach (var particle in _particles) {
            if (particle.Name == "particleName") {
                particle.Particle.Play();
            }
        }

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

[System.Serializable]
public struct Particles
{
    [SerializeField] private string _name;
    [SerializeField] private ParticleSystem _particle;

    // Optionally, you can add getter methods if needed
    public string Name => _name;
    public ParticleSystem Particle => _particle;
}
