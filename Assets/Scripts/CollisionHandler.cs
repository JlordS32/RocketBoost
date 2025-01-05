using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Finish":
                FinishCase();
                break;
            default:
                DefaultCase();
                break;
        }
    }

    private void DefaultCase()
    {
        ResetLevel();
    }

    private void FinishCase()
    {
        LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextLevel(int level)
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
