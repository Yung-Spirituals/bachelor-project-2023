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
    @Autowired
    private QuestionRepository questionRepository;
    @Autowired
    private LevelRepository levelRepository;

    public Long add(Question question, Level level)
    {
        if (questionRepository.findById(question.getId()).isPresent() ||
        levelRepository.findById(level.getId()).isEmpty()) return null;
        question.set_level(levelRepository.findById(level.getId()).get());
        return questionRepository.save(question).getId();
    }
    public void update(Question question)
    {
        if (questionRepository.findById(question.getId()).isEmpty()) return;
        Question questionInDatabase = questionRepository.findById(question.getId()).get();
        questionInDatabase.set_question(question.get_question());
        questionInDatabase.set_imageUrl(question.get_imageUrl());
        questionInDatabase.set_option0(question.get_option0());
        questionInDatabase.set_option1(question.get_option1());
        questionInDatabase.set_option2(question.get_option2());
        questionInDatabase.set_option3(question.get_option3());
        questionInDatabase.set_isOption0(question.get_isOption0());
        questionInDatabase.set_isOption1(question.get_isOption1());
        questionInDatabase.set_isOption2(question.get_isOption2());
        questionInDatabase.set_isOption3(question.get_isOption3());
        questionRepository.save(questionInDatabase);
    }

    public void delete(Question question)
    {
        if (questionRepository.findById(question.getId()).isPresent())
        {
            questionRepository.delete(questionRepository.findById(question.getId()).get());
            questionRepository.flush();
        }
    }

    public void deleteLevelQuestions(Level level)
    {
        List<Question> items = questionRepository.findAllBy_level_Id(level.getId());
        questionRepository.deleteAll(items);
        questionRepository.flush();
    }
}