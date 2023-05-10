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

    @PutMapping(value = "/add/story", consumes = {"*/*"})
    public ResponseEntity<?> addStory(@RequestBody JsonMultiObject jsonMultiObject)
    {
        return new ResponseEntity<>(storyService.add(jsonMultiObject.get_story()), HttpStatus.OK);
    }

    @PutMapping(value = "/add/level", consumes = {"*/*"})
    public ResponseEntity<?> addLevel(@RequestBody JsonMultiObject jsonMultiObject)
    {
        return new ResponseEntity<>(levelService.add(jsonMultiObject.get_level(), jsonMultiObject.get_story()), HttpStatus.OK);
    }

    @PutMapping(value = "/add/question", consumes = {"*/*"})
    public ResponseEntity<?> addQuestion(@RequestBody JsonMultiObject jsonMultiObject)
    {
        return new ResponseEntity<>(questionService.add(jsonMultiObject.get_question(), jsonMultiObject.get_level()), HttpStatus.OK);
    }

    @PutMapping(value = "/update/story", consumes = {"*/*"})
    public ResponseEntity<?> updateStory(@RequestBody JsonMultiObject jsonMultiObject)
    {
        storyService.update(jsonMultiObject.get_story());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PutMapping(value = "/update/level", consumes = {"*/*"})
    public ResponseEntity<?> updateLevel(@RequestBody JsonMultiObject jsonMultiObject)
    {
        levelService.update(jsonMultiObject.get_level());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PutMapping(value = "/update/question", consumes = {"*/*"})
    public ResponseEntity<?> update(@RequestBody JsonMultiObject jsonMultiObject)
    {
        questionService.update(jsonMultiObject.get_question());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping(value = "/delete/story", consumes = {"*/*"})
    public ResponseEntity<?> deleteStory(@RequestBody JsonMultiObject jsonMultiObject)
    {
        storyService.delete(jsonMultiObject.get_story());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping(value = "/delete/level", consumes = {"*/*"})
    public ResponseEntity<?> deleteLevel(@RequestBody JsonMultiObject jsonMultiObject)
    {
        levelService.delete(jsonMultiObject.get_level());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping(value = "/delete/question", consumes = {"*/*"})
    public ResponseEntity<?> deleteQuestion(@RequestBody JsonMultiObject jsonMultiObject)
    {
        questionService.delete(jsonMultiObject.get_question());
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
