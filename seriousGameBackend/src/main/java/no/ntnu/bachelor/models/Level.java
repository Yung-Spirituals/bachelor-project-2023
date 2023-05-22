package no.ntnu.bachelor.models;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity(name = "levels")
public class Level
{
  @Id
  @GeneratedValue
  private Long id;

  @JsonIgnore
  @ManyToOne
  @JoinColumn(name = "subject_id")
  private Subject subject;

  @OneToMany(mappedBy = "level", cascade = CascadeType.ALL, orphanRemoval = true)
  private Set<Question> questions = new HashSet<>();

  private String levelType;
  private String levelGoal;

  public Level() {}

  public Level(Subject subject, String levelType, String levelGoal)
  {
    this.subject = subject;
    this.levelType = levelType;
    this.levelGoal = levelGoal;
  }

  @Override
  public String toString()
  {
    return String.format(
            "Level[id=%d, subjectName='%s', levelType='%s', levelGoal='%s']",
            id, subject.getSubjectName(), levelType, levelGoal);
  }

  public Long getId() { return id; }

  //public Story getSubject() { return subject; }

  public void setSubject(Subject subject) { this.subject = subject; }

  public Set<Question> getQuestions() { return questions; }

  public void setQuestions(Set<Question> questions) { this.questions = questions; }

  public String getLevelType() { return levelType; }

  public void setLevelType(String levelType) { this.levelType = levelType; }

  public String getLevelGoal() { return levelGoal; }

  public void setLevelGoal(String levelGoal) { this.levelGoal = levelGoal; }
}
