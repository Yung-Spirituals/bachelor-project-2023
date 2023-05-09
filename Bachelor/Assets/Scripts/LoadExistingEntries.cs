﻿using System.Collections;
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

    public IEnumerator LoadEntries(List<Story> stories)
    {
        yield return ClearChildren();
        
        foreach (Story story in stories)
        {
            GameObject storyPreview = Instantiate(displayPrefab, parentTransform);
            StoryDisplay storyDisplay = storyPreview.GetComponent<StoryDisplay>();
            storyDisplay.story = story;
            storyDisplay.UpdateDisplay();
        }
        
        yield return true;
    }
    
    public IEnumerator LoadEntries(Story story)
    {
        yield return ClearChildren();
        
        text0.text = "Endre på tema";
        input0.text = story.StoryName;
        input1.text = story.StoryFullDescription;
        int i = 1;
        foreach (Level level in story.Levels) 
        { 
            GameObject levelPreview = Instantiate(displayPrefab, parentTransform);
            LevelDisplay levelDisplay = levelPreview.GetComponent<LevelDisplay>();
            levelDisplay.level = level;
            levelDisplay.SetLevelDisplayName("Nivå " + i);
            levelDisplay.UpdateDisplay();
            i++;
        }
        
        yield return true;
    }
    
    public IEnumerator LoadEntries(Level level)
    {
        yield return ClearChildren();

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

        yield return true;
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
        yield return true;
    }
}