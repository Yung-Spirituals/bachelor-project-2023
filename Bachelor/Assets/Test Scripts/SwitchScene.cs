using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test_Scripts
{
    public class SwitchScene : MonoBehaviour
    {
       public string scene;
        public void LoadNewScene(String sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadNewScene()
        {
            SceneManager.LoadScene(scene);
        }
    }
}
