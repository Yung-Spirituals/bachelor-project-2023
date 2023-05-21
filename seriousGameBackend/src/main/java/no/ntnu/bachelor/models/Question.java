package no.ntnu.bachelor.models;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;


@Entity(name = "questions")
public class Question
{
    @Id
    @GeneratedValue
    private Long id;

    @JsonIgnore
    @ManyToOne
    @JoinColumn(name = "level_id")
    private Level level;

    private String question;
    private String imageUrl;

    private String option0;
    private String option1;
    private String option2;
    private String option3;

    private boolean isOption0;
    private boolean isOption1;
    private boolean isOption2;
    private boolean isOption3;

    public Question() {}

    public Question(Level level, String question, String imageUrl,
                    String option0, String option1, String option2, String option3,
                    boolean isOption0, boolean isOption1, boolean isOption2, boolean isOption3)
    {
        this.level = level;
        this.question = question;
        this.imageUrl = imageUrl;

        this.option0 = option0;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;

        this.isOption0 = isOption0;
        this.isOption1 = isOption1;
        this.isOption2 = isOption2;
        this.isOption3 = isOption3;
    }

    @Override
    public String toString()
    {
        return String.format("Question[id=%d, levelName='%s', question='%s', imageUrl='%s', " +
                        "option0='%s', option1='%s', option2='%s', option3='%s', " +
                        "isOption0='%s', isOption1='%s', isOption2='%s', isOption3='%s']",
                id, level.getLevelName(), question, imageUrl,
                option0, option1, option2, option3,
                isOption0, isOption1, isOption2, isOption3);
    }

    public Long getId() { return id; }

    public Level getLevel() { return level; }

    public void setLevel(Level level) { this.level = level; }

    public String getQuestion() { return question; }

    public void setQuestion(String question) { this.question = question; }

    public String getImageUrl() { return imageUrl; }

    public void setImageUrl(String imageUrl) { this.imageUrl = imageUrl; }

    public String getOption0() { return option0; }

    public void setOption0(String option0) { this.option0 = option0; }

    public String getOption1() { return option1; }

    public void setOption1(String option1) { this.option1 = option1; }

    public String getOption2() { return option2; }

    public void setOption2(String option2) { this.option2 = option2; }

    public String getOption3() { return option3; }

    public void setOption3(String option3) { this.option3 = option3; }

    public boolean getIsOption0() { return isOption0; }

    public void setIsOption0(boolean isOption0) { this.isOption0 = isOption0; }

    public boolean getIsOption1() { return isOption1; }

    public void setIsOption1(boolean isOption1) { this.isOption1 = isOption1; }

    public boolean getIsOption2() { return isOption2; }

    public void setIsOption2(boolean isOption2) { this.isOption2 = isOption2; }

    public boolean getIsOption3() { return isOption3; }

    public void setIsOption3(boolean isOption3) { this.isOption3 = isOption3; }
}
