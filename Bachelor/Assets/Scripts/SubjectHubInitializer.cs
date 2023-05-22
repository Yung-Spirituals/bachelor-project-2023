using System.Collections;
using System.Collections.Generic;
using SoData;
using UnityEngine;

public class SubjectHubInitializer : MonoBehaviour
{
    [SerializeField] private GameObject subjectSelectButtonPrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private bool isAdmin;
    [SerializeField] private GameObject editGameDataButton;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private float timeout;
    
    private GameDataScriptableObject _scriptableObject;
    private bool _timedOut;

    // When the main menu is loaded, start a coroutine to await data from the backend.
    private void Start() { StartCoroutine(WaitForSubjects()); }

    // Displays a loading screen until data has been retrieved or timed out.
    private IEnumerator WaitForSubjects()
    {
        loadingScreen.SetActive(true);

        // Waits until either data is received or till a specified amount of time has passed.
        yield return WaitTillDoneOrTimeout();
        
        // Create a button for each retrieved subject.
        List<Subject> subjects = _scriptableObject.Subjects;
        foreach (Subject subject in subjects)
        {
            GameObject subjectPreview = Instantiate(subjectSelectButtonPrefab, parentTransform);
            SubjectDisplay subjectDisplay = subjectPreview.GetComponent<SubjectDisplay>();
            subjectDisplay.subject = subject;
            subjectDisplay.UpdateDisplay();
        }

        loadingScreen.SetActive(false);
        
        // Adds a button for entering a scene used for editing game data if isAdmin is true.
        if (isAdmin) { Instantiate(editGameDataButton, parentTransform); }
    }

    private IEnumerator WaitTillDoneOrTimeout()
    {
        _scriptableObject = GameDataManager.Instance.GetGameData();
        StartCoroutine(WaitTillTimeout());
        yield return new WaitUntil(() => _scriptableObject.Subjects.Count != 0 || _timedOut);
        yield return null;
    }

    private IEnumerator WaitTillTimeout()
    {
        yield return new WaitForSeconds(timeout);
        _timedOut = true;
        yield return null;
    }
}