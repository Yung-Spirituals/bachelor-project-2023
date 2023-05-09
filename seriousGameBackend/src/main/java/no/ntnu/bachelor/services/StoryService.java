package no.ntnu.bachelor.services;

import no.ntnu.bachelor.models.Story;
import no.ntnu.bachelor.repositories.StoryRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class StoryService
{
    @Autowired
    private StoryRepository storyRepository;

    public List<Story> getAllStories() { return storyRepository.findAll(); }

    public Long add(Story story)
    {
        if (storyRepository.findById(story.getId()).isPresent()) return null;
        return storyRepository.save(story).getId();
    }
    public void update(Story story)
    {
        if (storyRepository.findById(story.getId()).isEmpty()) return;
        Story storyInDatabase = storyRepository.findById(story.getId()).get();
        storyInDatabase.set_storyName(story.get_storyName());
        storyInDatabase.set_iconUrl(story.get_iconUrl());
        storyInDatabase.set_backgroundUrl(story.get_backgroundUrl());
        storyInDatabase.set_storyShortDescription(story.get_storyShortDescription());
        storyInDatabase.set_storyTitle(story.get_storyTitle());
        storyInDatabase.set_storyFullDescription(story.get_storyFullDescription());
        storyRepository.save(storyInDatabase);
    }

    public void delete(Story story)
    {
        if (storyRepository.findById(story.getId()).isEmpty()) return;
        storyRepository.delete(storyRepository.findById(story.getId()).get());
        storyRepository.flush();
    }
}
