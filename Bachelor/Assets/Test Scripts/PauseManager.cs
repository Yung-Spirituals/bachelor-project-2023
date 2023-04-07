using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public bool isPaused;
    [SerializeField] private GameObject pauseMenuUI;
    
    public static PauseManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(PauseManager)) as PauseManager;
 
            return instance;
        }
        set { instance = value; }
    }
    private static PauseManager instance;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
    }
}
