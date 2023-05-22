using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string scene;
    
    // Load specified scene.
    public void LoadNewScene(string sceneName) { SceneManager.LoadScene(sceneName); }
    
    // Load the scene held by the component
    public void LoadNewScene() { SceneManager.LoadScene(scene); }
}