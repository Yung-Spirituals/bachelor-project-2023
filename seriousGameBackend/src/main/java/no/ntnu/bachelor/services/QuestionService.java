package no.ntnu.bachelor.services;

import no.ntnu.bachelor.models.Level;
import no.ntnu.bachelor.models.Question;
import no.ntnu.bachelor.repositories.LevelRepository;
import no.ntnu.bachelor.repositories.QuestionRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class QuestionService
{
    @Autowired private QuestionRepository questionRepository;
    @Autowired private LevelRepository levelRepository;

    public Long add(Question question, Level level)
    {
        if (questionRepository.findById(question.getId()).isPresent() ||
        levelRepository.findById(level.getId()).isEmpty()) return null;

        question.setLevel(levelRepository.findById(level.getId()).get());

        return questionRepository.save(question).getId();
    }

    public void update(Question question)
    {
        if (questionRepository.findById(question.getId()).isEmpty()) return;

        Question questionInDatabase = questionRepository.findById(question.getId()).get();

        questionInDatabase.setQuestion(question.getQuestion());
        questionInDatabase.setImageUrl(question.getImageUrl());
        questionInDatabase.setOption0(question.getOption0());
        questionInDatabase.setOption1(question.getOption1());
        questionInDatabase.setOption2(question.getOption2());
        questionInDatabase.setOption3(question.getOption3());
        questionInDatabase.setIsOption0(question.getIsOption0());
        questionInDatabase.setIsOption1(question.getIsOption1());
        questionInDatabase.setIsOption2(question.getIsOption2());
        questionInDatabase.setIsOption3(question.getIsOption3());

        questionRepository.save(questionInDatabase);
    }

    public void delete(Long questionId)
    {
        if (questionRepository.findById(questionId).isEmpty()) return;

        questionRepository.delete(questionRepository.findById(questionId).get());
        questionRepository.flush();
    }

    public void deleteLevelQuestions(Level level)
    {
        List<Question> items = questionRepository.findAllByLevelId(level.getId());

        questionRepository.deleteAll(items);
        questionRepository.flush();
    }
}