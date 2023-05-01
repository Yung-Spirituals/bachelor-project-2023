package no.ntnu.bachelor.repositories;

import no.ntnu.bachelor.models.Level;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface LevelRepository extends JpaRepository<Level, Long>
{
    Level findById(long id);
    Level findBy_levelName(String _levelName);
}
