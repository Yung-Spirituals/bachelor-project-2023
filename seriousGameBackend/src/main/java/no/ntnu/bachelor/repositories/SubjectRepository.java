package no.ntnu.bachelor.repositories;

import no.ntnu.bachelor.models.Subject;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface SubjectRepository extends JpaRepository<Subject, Long>
{
    Subject findBySubjectName(String subjectName);
}