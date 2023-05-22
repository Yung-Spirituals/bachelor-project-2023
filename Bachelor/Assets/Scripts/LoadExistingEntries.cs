using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadExistingEntries : MonoBehaviour
{
    [SerializeField] private GameObject displayPrefab;
    [SerializeField] private Transform parentTransform;
    [SerializeField] private TextMeshProUGUI text0;
    [SerializeField] private TMP_InputField input0;
    [SerializeField] private TMP_InputField input1;
    [SerializeField] private TMP_Dropdown drop0;

    private Subject _subject;
    private Level _level;
    private Question _question;

    public IEnumerator LoadEntries(List<Subject> subjects)
    {
        yield return ClearChildren();
        
        foreach (Subject subject in subjects)
        {
            GameObject subjectPreview = Instantiate(displayPrefab, parentTransform);
            SubjectDisplay subjectDisplay = subjectPreview.GetComponent<SubjectDisplay>();
            subjectDisplay.subject = subject;
            subjectDisplay.UpdateDisplay();
        }
        
        yield return null;
    }
    
    public IEnumerator LoadEntries(Subject subject)
    {
        yield return ClearChildren();
        
        text0.text = "Endre på tema";
        input0.text = subject.SubjectName;
        input1.text = subject.SubjectDescription;
        int i = 1;
        foreach (Level level in subject.Levels) 
        { 
            GameObject levelPreview = Instantiate(displayPrefab, parentTransform);
            LevelDisplay levelDisplay = levelPreview.GetComponent<LevelDisplay>();
            levelDisplay.level = level;
            levelDisplay.SetLevelDisplayName("Nivå " + i);
            levelDisplay.UpdateDisplay();
            i++;
        }
        
        yield return null;
    }
    
    public IEnumerator LoadEntries(Level level)
    {
        yield return ClearChildren();

        drop0.value = CorrectDropDown(level.LevelType);
        text0.text = "Endre på nivå";
        input0.text = level.LevelGoal;
        int counter = 1;
        foreach (Question question in level.Questions)
        {
            GameObject questionPreview = Instantiate(displayPrefab, parentTransform);
            QuestionDisplay questionDisplay = questionPreview.GetComponent<QuestionDisplay>();
            questionDisplay.question = question;
            if (level.LevelType == GameMode.MemoryCards)
            {
                questionDisplay.UpdateDisplay("Par " + counter);
                counter++;
            }
            else questionDisplay.UpdateDisplay(question.QuestionText);
        }

        yield return null;
    }

    private static int CorrectDropDown(string levelType)
    {
        return levelType switch
        {
            GameMode.Standard => 0,
            GameMode.TrueOrFalse => 1,
            GameMode.Rank => 2,
            GameMode.MemoryCards => 3,
            _ => 0
        };
    }

    private IEnumerator ClearChildren()
    {
        foreach (Transform child in parentTransform) Destroy(child.gameObject);
        yield return null;
    }
}