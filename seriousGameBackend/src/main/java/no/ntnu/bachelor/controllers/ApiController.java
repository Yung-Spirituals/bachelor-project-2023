package no.ntnu.bachelor.controllers;

import no.ntnu.bachelor.models.Story;
import no.ntnu.bachelor.models.StoryCollection;
import no.ntnu.bachelor.services.StoryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/story")
public class ApiController
{
    @Autowired
    private StoryService storyService;

    @GetMapping("/stories")
    public ResponseEntity<?> getStories()
    {
        StoryCollection storyCollection = new StoryCollection();
        storyCollection.set_stories(storyService.getAllStories());
        return new ResponseEntity<>(storyCollection, HttpStatus.OK);
    }

    @PostMapping("/add-story")
    public ResponseEntity<?> addStory(@RequestBody Story story)
    {
        storyService.addNewStory(story);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @PostMapping("/update-story")
    public ResponseEntity<?> updateStory(@RequestBody Story story)
    {
        storyService.updateStory(story);
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping("/delete-story")
    public ResponseEntity<?> deleteStory(@RequestBody Story story)
    {
        storyService.deleteStory(story);
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
