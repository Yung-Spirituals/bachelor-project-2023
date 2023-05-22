using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public bool isPaused;
    [SerializeField] private GameObject pauseMenuUI;
    
    public static PauseManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType(typeof(PauseManager)) as PauseManager;
 
            return _instance;
        }
        set => _instance = value;
    }
    private static PauseManager _instance;

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
    }
}