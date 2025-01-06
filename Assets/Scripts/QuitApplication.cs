using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    private void Update() {
        if (Application.isPlaying && Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("Quitting game");
            Application.Quit();
        }
    }
}
