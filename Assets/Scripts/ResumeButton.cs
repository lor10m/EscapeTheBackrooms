using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeButton : MonoBehaviour
{
    public void OnResumeButtonClicked()
    {
        Time.timeScale = 1f; // Set the time scale to resume the game
        Cursor.visible = false; // Hide the cursor if needed
        SceneManager.UnloadSceneAsync(2); // Unload the pause scene
        
    }
}
