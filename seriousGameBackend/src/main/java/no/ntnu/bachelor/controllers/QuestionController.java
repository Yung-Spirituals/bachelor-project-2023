package no.ntnu.bachelor.controllers;

import no.ntnu.bachelor.models.JsonMultiObject;
import no.ntnu.bachelor.services.QuestionService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/question")
public class QuestionController
{
    @Autowired private QuestionService questionService;

    @PutMapping(value = "/add", consumes = {"*/*"})
    public ResponseEntity<?> add(@RequestBody JsonMultiObject jsonMultiObject)
    {
        return new ResponseEntity<>(questionService.add(jsonMultiObject.getQuestion(),
                jsonMultiObject.getLevel()), HttpStatus.OK);
    }

    @PutMapping(value = "/update", consumes = {"*/*"})
    public ResponseEntity<?> update(@RequestBody JsonMultiObject jsonMultiObject)
    {
        questionService.update(jsonMultiObject.getQuestion());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping(value = "/delete/{id}")
    public ResponseEntity<?> delete(@PathVariable Long id)
    {
        questionService.delete(id);
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
