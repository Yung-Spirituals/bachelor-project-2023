package no.ntnu.bachelor.models;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity(name = "stories")
public class Story
{
    @Id
    @GeneratedValue
    private Long id;

    @OneToMany(mappedBy = "_story", cascade = CascadeType.ALL, orphanRemoval = true)
    private Set<Level> _levels = new HashSet<>();

    private String _storyName;
    private String _iconUrl;
    private String _backgroundUrl;
    private String _storyShortDescription;
    private String _storyTitle;
    private String _storyFullDescription;

    public Story() {}

    public Story(String storyName, String iconUrl, String backgroundUrl,
                 String storyShortDescription, String storyTitle, String storyFullDescription)
    {
        _storyName = storyName;
        _iconUrl = iconUrl;
        _backgroundUrl = backgroundUrl;
        _storyShortDescription = storyShortDescription;
        _storyTitle = storyTitle;
        _storyFullDescription = storyFullDescription;
    }

    @Override
    public String toString()
    {
        return String.format("Level[id=%d, " +
                        "storyName='%s', iconUrl='%s', backgroundUrl='%s', " +
                        "storyShortDescription='%s', storyTitle='%s', storyFullDescription='%s']",
                id, _storyName, _iconUrl, _backgroundUrl, _storyShortDescription, _storyTitle, _storyFullDescription);
    }

    public Long getId() { return id; }

    public Set<Level> get_levels() {return _levels;}

    public void set_levels(Set<Level> levelSet) { _levels = levelSet; }

    public String get_storyName() { return _storyName; }

    public void set_storyName(String storyName) { _storyName = storyName; }

    public String get_iconUrl() { return _iconUrl; }

    public void set_iconUrl(String iconUrl) { _iconUrl = iconUrl; }

    public String get_backgroundUrl() { return _backgroundUrl; }

    public void set_backgroundUrl(String backgroundUrl) { _backgroundUrl = backgroundUrl; }

    public String get_storyShortDescription() { return _storyShortDescription; }

    public void set_storyShortDescription(String storyShortDescription) { _storyShortDescription = storyShortDescription; }

    public String get_storyTitle() { return _storyTitle; }

    public void set_storyTitle(String storyTitle) { _storyTitle = storyTitle; }

    public String get_storyFullDescription() { return _storyFullDescription; }

    public void set_storyFullDescription(String storyFullDescription) { _storyFullDescription = storyFullDescription; }
}
