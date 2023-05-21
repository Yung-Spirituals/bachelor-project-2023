package no.ntnu.bachelor.models;

import javax.persistence.*;
import java.util.HashSet;
import java.util.Set;

@Entity(name = "subjects")
public class Subject
{
    @Id
    @GeneratedValue
    private Long id;

    @OneToMany(mappedBy = "subject", cascade = CascadeType.ALL, orphanRemoval = true)
    private Set<Level> levels = new HashSet<>();

    private String subjectName;
    private String backgroundUrl;
    private String subjectDescription;

    public Subject() {}

    public Subject(String subjectName,  String backgroundUrl, String subjectDescription)
    {
        this.subjectName = subjectName;
        this.backgroundUrl = backgroundUrl;
        this.subjectDescription = subjectDescription;
    }

    @Override
    public String toString()
    {
        return String.format("Level[id=%d, subjectName='%s', backgroundUrl='%s', subjectDescription='%s']",
                id, subjectName, backgroundUrl, subjectDescription);
    }

    public Long getId() { return id; }

    public Set<Level> getLevels() {return levels;}

    public void setLevels(Set<Level> levels) { this.levels = levels; }

    public String getSubjectName() { return subjectName; }

    public void setSubjectName(String subjectName) { this.subjectName = subjectName; }

    public String getBackgroundUrl() { return backgroundUrl; }

    public void setBackgroundUrl(String backgroundUrl) { this.backgroundUrl = backgroundUrl; }

    public String getSubjectDescription() { return subjectDescription; }

    public void setSubjectDescription(String subjectDescription) { this.subjectDescription = subjectDescription; }
}
