using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadNewScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
