package no.ntnu.bachelor.repositories;

import no.ntnu.bachelor.models.Question;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface QuestionRepository extends JpaRepository<Question, Long>
{
    Question findById(long id);
    Question findBy_question(String _question);
}