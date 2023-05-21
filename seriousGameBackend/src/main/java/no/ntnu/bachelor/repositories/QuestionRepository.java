package no.ntnu.bachelor.repositories;

import no.ntnu.bachelor.models.Question;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface QuestionRepository extends JpaRepository<Question, Long>
{
    List<Question> findAllByLevelId(Long levelId);
}