using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string scene;
    public void LoadNewScene(string sceneName) { SceneManager.LoadScene(sceneName); }
    public void LoadNewScene() { SceneManager.LoadScene(scene); }
}