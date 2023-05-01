package no.ntnu.bachelor.services;

import no.ntnu.bachelor.models.Story;
import no.ntnu.bachelor.repositories.StoryRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class StoryService {

    @Autowired
    private StoryRepository storyRepository;

    public List<Story> getAllStories() { return storyRepository.findAll(); }

    public Story getStoryById(Long id)
    {
        return storyRepository.findById(id).isPresent() ? storyRepository.findById(id).get() : null;
    }

    public void addNewStory(String storyName, String iconUrl,String backgroundUrl,
                            String storyShortDescription, String storyTitle, String storyFullDescription)
    {
        storyRepository.save(new Story(storyName, iconUrl, backgroundUrl,
                storyShortDescription, storyTitle, storyFullDescription));
    }

    public void updateStory(Story updatedStory)
    {
        Story story = getStoryById(updatedStory.getId());
        if (story == null) { addNewStory(updatedStory); }
        else
        {
            story.set_levels(updatedStory.get_levels());
            story.set_storyName(updatedStory.get_storyName());
            story.set_iconUrl(updatedStory.get_iconUrl());
            story.set_backgroundUrl(updatedStory.get_backgroundUrl());
            story.set_storyShortDescription(updatedStory.get_storyShortDescription());
            story.set_storyTitle(updatedStory.get_storyTitle());
            story.set_storyFullDescription(updatedStory.get_storyFullDescription());
            storyRepository.save(story);
        }
    }

    /**
     *
     */
    public void addNewStory(Story story) { storyRepository.save(story); }

    public void deleteStory(Long id)
    {
        storyRepository.delete(storyRepository.getById(id));
        storyRepository.flush();
    }

    public void deleteStory(Story story)
    {
        storyRepository.delete(storyRepository.getById(story.getId()));
        storyRepository.flush();
    }

    public void deleteAllStories()
    {
        storyRepository.deleteAll();
        storyRepository.flush();
    }
}
