package no.ntnu.bachelor.services;

import no.ntnu.bachelor.models.Level;
import no.ntnu.bachelor.models.Subject;
import no.ntnu.bachelor.repositories.LevelRepository;
import no.ntnu.bachelor.repositories.SubjectRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class LevelService
{
    @Autowired private LevelRepository levelRepository;
    @Autowired private SubjectRepository subjectRepository;
    @Autowired private QuestionService questionService;

    public Long add(Level level, Subject subject)
    {
        if (levelRepository.findById(level.getId()).isEmpty() &&
                subjectRepository.findById(subject.getId()).isEmpty()) return null;

        level.setSubject(subjectRepository.findById(subject.getId()).get());

        return levelRepository.save(level).getId();
    }
    public void update(Level level)
    {
        if (levelRepository.findById(level.getId()).isEmpty()) return;

        Level levelInDatabase = levelRepository.findById(level.getId()).get();

        if (!levelInDatabase.getLevelType().equals(level.getLevelType()))
            questionService.deleteLevelQuestions(levelInDatabase);

        levelInDatabase.setLevelType(level.getLevelType());
        levelInDatabase.setLevelName(level.getLevelName());
        levelInDatabase.setLevelGoal(level.getLevelGoal());

        levelRepository.save(levelInDatabase);
    }

    public void delete(Level level)
    {
        if (levelRepository.findById(level.getId()).isEmpty()) return;

        levelRepository.delete(levelRepository.findById(level.getId()).get());
        levelRepository.flush();
    }
}