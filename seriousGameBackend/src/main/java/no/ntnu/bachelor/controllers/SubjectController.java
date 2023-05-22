package no.ntnu.bachelor.controllers;

import no.ntnu.bachelor.models.JsonMultiObject;
import no.ntnu.bachelor.models.SubjectCollection;
import no.ntnu.bachelor.services.SubjectService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/subject")
public class SubjectController
{
    @Autowired private SubjectService subjectService;

    // Send all subjects in the database.
    @GetMapping("/subjects")
    public ResponseEntity<?> getStories()
    {
        SubjectCollection subjectCollection = new SubjectCollection();
        subjectCollection.set_subjects(subjectService.getAllStories());
        return new ResponseEntity<>(subjectCollection, HttpStatus.OK);
    }

    // Add a new subject to the database.
    @PutMapping(value = "/add", consumes = {"*/*"})
    public ResponseEntity<?> add(@RequestBody JsonMultiObject jsonMultiObject)
    {
        return new ResponseEntity<>(subjectService.add(jsonMultiObject.getSubject()), HttpStatus.OK);
    }

    // Update an existing subject in the database.
    @PutMapping(value = "/update", consumes = {"*/*"})
    public ResponseEntity<?> update(@RequestBody JsonMultiObject jsonMultiObject)
    {
        subjectService.update(jsonMultiObject.getSubject());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    // Delete a subject from the database.
    @DeleteMapping(value = "/delete/{id}")
    public ResponseEntity<?> delete(@PathVariable Long id)
    {
        subjectService.delete(id);
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
