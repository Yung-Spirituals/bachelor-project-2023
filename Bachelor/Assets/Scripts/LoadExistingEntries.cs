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

    private Story _story;
    private Level _level;
    private Question _question;

    public void LoadEntries(List<Story> stories)
    {
        ClearChildren();
        foreach (Story story in stories)
        {
            GameObject storyPreview = Instantiate(displayPrefab, parentTransform);
            StoryDisplay storyDisplay = storyPreview.GetComponent<StoryDisplay>();
            storyDisplay.story = story;
            storyDisplay.UpdateDisplay();
        }
    }
    
    public void LoadEntries(Story story)
    {
        ClearChildren();
        
        text0.text = "Endre på tema";
        input0.text = story.StoryName;
        input1.text = story.StoryFullDescription;
        foreach (Level level in story.Levels) 
        { 
            GameObject levelPreview = Instantiate(displayPrefab, parentTransform);
            LevelDisplay levelDisplay = levelPreview.GetComponent<LevelDisplay>();
            levelDisplay.level = level;
            levelDisplay.UpdateDisplay();
        }
    }
    
    public void LoadEntries(Level level)
    {
        ClearChildren();

        drop0.value = CorrectDropDown(level.LevelType);
        text0.text = "Endre på nivå";
        input0.text = level.LevelGoal;
        foreach (Question question in level.Questions)
        {
            GameObject questionPreview = Instantiate(displayPrefab, parentTransform);
            QuestionDisplay questionDisplay = questionPreview.GetComponent<QuestionDisplay>();
            questionDisplay.question = question;
            questionDisplay.UpdateDisplay();
        }
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

    private void ClearChildren()
    {
        foreach (Transform child in parentTransform.transform) { Destroy(child.gameObject); }
    }
}