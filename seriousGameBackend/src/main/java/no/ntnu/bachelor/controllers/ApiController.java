package no.ntnu.bachelor.controllers;

import no.ntnu.bachelor.models.JsonMultiObject;
import no.ntnu.bachelor.models.StoryCollection;
import no.ntnu.bachelor.services.LevelService;
import no.ntnu.bachelor.services.QuestionService;
import no.ntnu.bachelor.services.StoryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/game")
public class ApiController
{
    @Autowired
    private StoryService storyService;
    @Autowired
    private LevelService levelService;
    @Autowired
    private QuestionService questionService;

    @GetMapping("/stories")
    public ResponseEntity<?> getStories()
    {
        StoryCollection storyCollection = new StoryCollection();
        storyCollection.set_stories(storyService.getAllStories());
        return new ResponseEntity<>(storyCollection, HttpStatus.OK);
    }

    @PostMapping("/add")
    public ResponseEntity<?> add(@RequestBody JsonMultiObject jsonMultiObject)
    {
        if (jsonMultiObject.get_question() != null && jsonMultiObject.get_level() != null)
        {
            questionService.add(jsonMultiObject.get_question(), jsonMultiObject.get_level());
        }
        else if (jsonMultiObject.get_level() != null && jsonMultiObject.get_story() != null)
        {
            levelService.add(jsonMultiObject.get_level(), jsonMultiObject.get_story());
        }
        else if (jsonMultiObject.get_story() != null)
        {
            storyService.add(jsonMultiObject.get_story());
        }
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PostMapping("/update")
    public ResponseEntity<?> update(@RequestBody JsonMultiObject jsonMultiObject)
    {
        if (jsonMultiObject.get_question() != null)
        {
            questionService.update(jsonMultiObject.get_question());
        }
        else if (jsonMultiObject.get_level() != null)
        {
            levelService.update(jsonMultiObject.get_level());
        }
        else if (jsonMultiObject.get_story() != null)
        {
            storyService.update(jsonMultiObject.get_story());
        }
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/delete")
    public ResponseEntity<?> delete(@RequestBody JsonMultiObject jsonMultiObject)
    {
        if (jsonMultiObject.get_question() != null)
        {
            questionService.delete(jsonMultiObject.get_question());
        }
        else if (jsonMultiObject.get_level() != null)
        {
            levelService.delete(jsonMultiObject.get_level());
        }
        else if (jsonMultiObject.get_story() != null)
        {
            storyService.delete(jsonMultiObject.get_story());
        }
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
