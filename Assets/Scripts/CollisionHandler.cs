using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag) {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                Debug.Log("Done!");
                break;
            case "Fuel":
                Debug.Log("Recharge");
                break;
            case "Enemy":
                EnemyCase();
                break;
            default:
                break;
        }
    }

    private void EnemyCase() {
        ReloadLevel(0);
    }

    private void ReloadLevel(int level) {
        SceneManager.LoadScene(level);
    }
}
