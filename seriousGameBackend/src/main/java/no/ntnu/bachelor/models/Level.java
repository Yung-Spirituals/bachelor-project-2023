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
  @JoinColumn(name = "story_id")
  private Story _story;

  @OneToMany(mappedBy = "_level", cascade = CascadeType.ALL, orphanRemoval = true)
  private Set<Question> _questions = new HashSet<>();

  private String _levelName;
  private String _backgroundUrl;
  private String _levelType;
  private String _levelGoal;
  private String _howToPlay;

  public Level() {}

  public Level(Story story, String levelName, String backgroundUrl, String levelType, String levelGoal, String howToPlay)
  {
    _story = story;
    _levelName = levelName;
    _backgroundUrl = backgroundUrl;
    _levelType = levelType;
    _levelGoal = levelGoal;
    _howToPlay = howToPlay;
  }

  @Override
  public String toString()
  {
    return String.format("Level[id=%d, storyName='%s', levelName='%s', backgroundUrl='%s', levelType='%s', levelGoal='%s', howToPlay='%s']",
            id, _story.get_storyName(), _levelName, _backgroundUrl, _levelType, _levelGoal, _howToPlay);
  }

  public Long getId() { return id; }

  public void setId(Long id) { this.id = id; }

  //public Story get_story() { return _story; }

  public void set_story(Story story) { _story = story; }

  public Set<Question> get_questions() { return _questions; }

  public void set_questions(Set<Question> questions) { _questions = questions; }

  public String get_levelName() { return _levelName; }

  public void set_levelName(String levelName) { _levelName = levelName; }

  public String get_backgroundUrl() { return _backgroundUrl; }

  public void set_backgroundUrl(String backgroundUrl) { _backgroundUrl = backgroundUrl; }

  public String get_levelType() { return _levelType; }

  public void set_levelType(String levelType) { _levelType = levelType; }

  public String get_levelGoal() { return _levelGoal; }

  public void set_levelGoal(String levelGoal) { _levelGoal = levelGoal; }

  public String get_howToPlay() { return _howToPlay; }

  public void set_howToPlay(String howToPlay) { _howToPlay = howToPlay; }
}
