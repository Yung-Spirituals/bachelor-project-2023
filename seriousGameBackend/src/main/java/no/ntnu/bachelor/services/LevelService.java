package no.ntnu.bachelor.services;

import no.ntnu.bachelor.models.Level;
import no.ntnu.bachelor.models.Story;
import no.ntnu.bachelor.repositories.LevelRepository;
import no.ntnu.bachelor.repositories.StoryRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class LevelService
{
    @Autowired
    private LevelRepository levelRepository;

    @Autowired
    private StoryRepository storyRepository;

    @Autowired
    private QuestionService questionService;

    public void add(Level level, Story story)
    {
        if (levelRepository.findById(level.getId()).isPresent() ||
                storyRepository.findById(story.getId()).isEmpty()) return;
        level.set_story(storyRepository.findById(story.getId()).get());
        levelRepository.save(level);
    }
    public void update(Level level)
    {
        if (levelRepository.findById(level.getId()).isEmpty()) return;
        Level levelInDatabase = levelRepository.findById(level.getId()).get();

        if (!levelInDatabase.get_levelType().equals(level.get_levelType()))
        {
            questionService.deleteLevelQuestions(levelInDatabase);
        }

        levelInDatabase.set_levelType(level.get_levelType());
        levelInDatabase.set_levelName(level.get_levelName());
        levelInDatabase.set_levelGoal(level.get_levelGoal());
        levelInDatabase.set_backgroundUrl(level.get_backgroundUrl());
        levelInDatabase.set_howToPlay(level.get_howToPlay());
        levelRepository.save(levelInDatabase);
    }

    public void delete(Level level)
    {
        if (levelRepository.findById(level.getId()).isPresent())
        {
            levelRepository.delete(levelRepository.findById(level.getId()).get());
            levelRepository.flush();
        }
    }
}