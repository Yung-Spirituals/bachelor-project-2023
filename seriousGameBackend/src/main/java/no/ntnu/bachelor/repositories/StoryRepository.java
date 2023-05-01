package no.ntnu.bachelor.repositories;

import no.ntnu.bachelor.models.Story;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface StoryRepository extends JpaRepository<Story, Long>
{
    Story findById(long id);
    Story findBy_storyName(String _storyName);
}