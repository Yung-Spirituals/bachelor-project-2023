package no.ntnu.bachelor.controllers;

import no.ntnu.bachelor.models.JsonMultiObject;
import no.ntnu.bachelor.services.LevelService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@CrossOrigin
@RequestMapping("/level")
public class LevelController
{
    @Autowired private LevelService levelService;

    @PutMapping(value = "/add", consumes = {"*/*"})
    public ResponseEntity<?> add(@RequestBody JsonMultiObject jsonMultiObject)
    {
        return new ResponseEntity<>(levelService.add(jsonMultiObject.getLevel(),
                jsonMultiObject.getSubject()), HttpStatus.OK);
    }

    @PutMapping(value = "/update", consumes = {"*/*"})
    public ResponseEntity<?> update(@RequestBody JsonMultiObject jsonMultiObject)
    {
        levelService.update(jsonMultiObject.getLevel());
        return new ResponseEntity<>(HttpStatus.OK);
    }

    @DeleteMapping(value = "/delete", consumes = {"*/*"})
    public ResponseEntity<?> delete(@RequestBody JsonMultiObject jsonMultiObject)
    {
        levelService.delete(jsonMultiObject.getLevel());
        return new ResponseEntity<>(HttpStatus.OK);
    }
}
