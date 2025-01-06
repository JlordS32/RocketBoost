using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay = 2f;
    [SerializeField] private AudioClip _crash;
    [SerializeField] private AudioClip _success;

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
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        _isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crash);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ResetLevel), _levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        _isControllable = false;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_success);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
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
