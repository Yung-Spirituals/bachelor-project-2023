package no.ntnu.bachelor.services;

import no.ntnu.bachelor.models.Subject;
import no.ntnu.bachelor.repositories.SubjectRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SubjectService
{
    @Autowired private SubjectRepository subjectRepository;

    public List<Subject> getAllStories() { return subjectRepository.findAll(); }

    public Long add(Subject subject)
    {
        if (subjectRepository.findById(subject.getId()).isPresent()) return null;

        return subjectRepository.save(subject).getId();
    }
    public void update(Subject subject)
    {
        if (subjectRepository.findById(subject.getId()).isEmpty()) return;

        Subject subjectInDatabase = subjectRepository.findById(subject.getId()).get();

        subjectInDatabase.setSubjectName(subject.getSubjectName());
        subjectInDatabase.setBackgroundUrl(subject.getBackgroundUrl());
        subjectInDatabase.setSubjectDescription(subject.getSubjectDescription());

        subjectRepository.save(subjectInDatabase);
    }

    public void delete(Subject subject)
    {
        if (subjectRepository.findById(subject.getId()).isEmpty()) return;

        subjectRepository.delete(subjectRepository.findById(subject.getId()).get());
        subjectRepository.flush();
    }
}
