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

    // Return all subjects found in the database.
    public List<Subject> getAllStories() { return subjectRepository.findAll(); }

    // Add a new subject to the database.
    public Long add(Subject subject)
    {
        // Check that there are no subjects in the database with that id. (new subjects should not have any id).
        if (subjectRepository.findById(subject.getId()).isPresent()) return null;

        // Save the new subject to the database.
        return subjectRepository.save(subject).getId();
    }

    // Update an existing subject.
    public void update(Subject subject)
    {
        // Check that the subject is in the database.
        if (subjectRepository.findById(subject.getId()).isEmpty()) return;

        // Get the subject from the database.
        Subject subjectInDatabase = subjectRepository.findById(subject.getId()).get();

        // Sets the attributes of the subject to the newly provided ones.
        subjectInDatabase.setSubjectName(subject.getSubjectName());
        subjectInDatabase.setBackgroundUrl(subject.getBackgroundUrl());
        subjectInDatabase.setSubjectDescription(subject.getSubjectDescription());

        // Save the updated subject to the database.
        subjectRepository.save(subjectInDatabase);
    }

    // Delete an existing subject from the database.
    public void delete(Long subjectId)
    {
        // Check that there is a subject with that id in the database.
        if (subjectRepository.findById(subjectId).isEmpty()) return;

        // Delete the subject from the database.
        subjectRepository.delete(subjectRepository.findById(subjectId).get());
        subjectRepository.flush();
    }
}
